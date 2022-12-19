using System.Net.Http.Headers;
using System.Net;
using Pantry.Mobile.Core.Infrastructure.Abstractions;

namespace Pantry.Mobile.Core.Infrastructure.Auth0;

/// <summary>
/// HTTP message delegating handler that encapsulates token handling and refresh
/// Copied from https://github.com/IdentityModel/IdentityModel.OidcClient/blob/main/src/OidcClient/RefreshTokenDelegatingHandler.cs.
/// </summary>
public class RefreshTokenDelegatingHandler : DelegatingHandler
{
    private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
    private readonly IAuth0Client _auth0Client;
    private readonly ISettingsService _settingsService;

    private string? _accessToken;
    private string? _refreshToken;

    private bool _disposed;

    /// <summary>
    /// Gets or sets the timeout
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Gets the current access token
    /// </summary>
    public string? AccessToken
    {
        get
        {
            if (_lock.Wait(Timeout))
            {
                try
                {
                    return _accessToken;
                }
                finally
                {
                    _lock.Release();
                }
            }

            return null;
        }
    }

    /// <summary>
    /// Gets the current refresh token
    /// </summary>
    public string? RefreshToken
    {
        get
        {
            if (_lock.Wait(Timeout))
            {
                try
                {
                    return _refreshToken;
                }
                finally
                {
                    _lock.Release();
                }
            }

            return null;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenDelegatingHandler" /> class.
    /// </summary>
    /// <param name="oidcClient">The oidc client.</param>
    /// <param name="accessToken">The access token.</param>
    /// <param name="refreshToken">The refresh token.</param>
    /// <param name="innerHandler">The inner handler.</param>
    /// <exception cref="ArgumentNullException">oidcClient</exception>
    public RefreshTokenDelegatingHandler(IAuth0Client auth0Client, ISettingsService settingsService, HttpMessageHandler? innerHandler = null)
    {
        _auth0Client = auth0Client ?? throw new ArgumentNullException(nameof(auth0Client));
        _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));

        InnerHandler = innerHandler ?? new HttpClientHandler();
    }

    /// <summary>
    /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
    /// </summary>
    /// <param name="request">The HTTP request message to send to the server.</param>
    /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
    /// <returns>
    /// Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.
    /// </returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_accessToken))
        {
            var credentials = await _settingsService.GetCredentials();
            if (credentials != null)
            {
                _accessToken = credentials.AccessToken;
                _refreshToken = credentials.RefreshToken;
            }
        }

        var accessToken = await GetAccessTokenAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            if (await RefreshTokensAsync(cancellationToken) == false)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized) { RequestMessage = request };
            }
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

        if (response.StatusCode != HttpStatusCode.Unauthorized)
        {
            return response;
        }

        if (await RefreshTokensAsync(cancellationToken) == false)
        {
            return response;
        }

        response.Dispose(); // This 401 response will not be used for anything so is disposed to unblock the socket.

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Net.Http.DelegatingHandler" />, and optionally disposes of the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && !_disposed)
        {
            _disposed = true;
            _lock.Dispose();
        }

        base.Dispose(disposing);
    }

    private async Task<bool> RefreshTokensAsync(CancellationToken cancellationToken)
    {
        if (await _lock.WaitAsync(Timeout, cancellationToken).ConfigureAwait(false))
        {
            if (string.IsNullOrWhiteSpace(_refreshToken))
            {
                return false;
            }

            try
            {
                var response = await _auth0Client.RefreshToken(_refreshToken, cancellationToken: cancellationToken).ConfigureAwait(false);

                if (!response.HasError)
                {
                    _accessToken = response.AccessToken;
                    if (!string.IsNullOrWhiteSpace(response.RefreshToken))
                    {
                        _refreshToken = response.RefreshToken;
                    }

                    await _settingsService.SetCredentials(response);

                    return true;
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        return false;
    }

    private async Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken)
    {
        if (await _lock.WaitAsync(Timeout, cancellationToken).ConfigureAwait(false))
        {
            try
            {
                return _accessToken;
            }
            finally
            {
                _lock.Release();
            }
        }

        return null;
    }
}
