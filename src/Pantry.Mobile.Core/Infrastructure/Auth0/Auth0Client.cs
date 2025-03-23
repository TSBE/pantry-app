using Duende.IdentityModel.OidcClient;
using Duende.IdentityModel.OidcClient.Browser;
using Duende.IdentityModel.Client;
using Duende.IdentityModel.OidcClient.Results;
using Pantry.Mobile.Core.Infrastructure.Abstractions;

namespace Pantry.Mobile.Core.Infrastructure.Auth0;

public class Auth0Client : IAuth0Client
{
    private readonly OidcClient oidcClient;
    private readonly Auth0ClientOptions _auth0ClientOptions;

    public Auth0Client(Auth0ClientOptions options)
    {
        _auth0ClientOptions = options;
        oidcClient = new OidcClient(new OidcClientOptions
        {
            Authority = $"https://{options.Domain}",
            ClientId = options.ClientId,
            Scope = options.Scope,
            RedirectUri = options.RedirectUri,
            Browser = options.Browser
        });
    }

    public async Task<Credentials> Login()
    {
        try
        {
            var requestUrl = new LoginRequest();
            requestUrl.FrontChannelExtraParameters.Add("audience", _auth0ClientOptions.Audience);
            var loginResult = await oidcClient.LoginAsync(requestUrl);
            return loginResult.ToCredentials();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new Credentials { Error = ex.ToString() };
        }
    }

    public async Task<Credentials> Signup()
    {
        try
        {
            var requestUrl = new LoginRequest();
            requestUrl.FrontChannelExtraParameters.Add("prompt", "login");
            requestUrl.FrontChannelExtraParameters.Add("screen_hint", "signup");
            requestUrl.FrontChannelExtraParameters.Add("audience", _auth0ClientOptions.Audience);
            var loginResult = await oidcClient.LoginAsync(requestUrl);
            return loginResult.ToCredentials();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new Credentials { Error = ex.ToString() };
        }
    }

    public async Task<Credentials> RefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        try
        {
            var refreshTokenResult = await oidcClient.RefreshTokenAsync(refreshToken, cancellationToken: cancellationToken);
            return refreshTokenResult.ToCredentials(refreshToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new Credentials { Error = ex.ToString() };
        }
    }

    public async Task<UserInfoResult> GetUserInfo(string accessToken)
    {
        return await oidcClient.GetUserInfoAsync(accessToken);
    }

    public async Task<BrowserResult> Logout()
    {
        var logoutParameters = new Dictionary<string, string>
        {
            {"client_id", oidcClient.Options.ClientId },
            {"returnTo", oidcClient.Options.RedirectUri }
        };

        var logoutRequest = new LogoutRequest();
        var endSessionUrl = new RequestUrl($"{oidcClient.Options.Authority}/v2/logout")
          .Create(new Parameters(logoutParameters));
        var browserOptions = new BrowserOptions(endSessionUrl, oidcClient.Options.RedirectUri)
        {
            Timeout = TimeSpan.FromSeconds(logoutRequest.BrowserTimeout),
            DisplayMode = logoutRequest.BrowserDisplayMode
        };

        var browserResult = await oidcClient.Options.Browser.InvokeAsync(browserOptions);

        return browserResult;
    }
}
