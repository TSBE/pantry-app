using Duende.IdentityModel.Client;
using Duende.IdentityModel.OidcClient.Browser;

namespace Pantry.Mobile.Authenticator;

public class WebBrowserAuthenticator : Duende.IdentityModel.OidcClient.Browser.IBrowser
{
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        try
        {
            WebAuthenticatorResult result = await WebAuthenticator.Default.AuthenticateAsync(            new WebAuthenticatorOptions
            {
                CallbackUrl = new Uri(options.EndUrl),
                Url = new Uri(options.StartUrl),
                PrefersEphemeralWebBrowserSession = true
            });

            var url = new RequestUrl(options.EndUrl)
                .Create(new Parameters(result.Properties));

            return new BrowserResult
            {
                Response = url,
                ResultType = BrowserResultType.Success
            };
        }
        catch (TaskCanceledException)
        {
            return new BrowserResult
            {
                ResultType = BrowserResultType.UserCancel,
                ErrorDescription = "Login canceled by the user."
            };
        }
    }
}
