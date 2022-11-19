using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using IdentityModel.Client;

namespace Pantry.Mobile.Core.Infrastructure.Auth0;

public class Auth0Client
{
    private readonly OidcClient oidcClient;

    public Auth0Client(Auth0ClientOptions options)
    {
        oidcClient = new OidcClient(new OidcClientOptions
        {
            Authority = $"https://{options.Domain}",
            ClientId = options.ClientId,
            Scope = options.Scope,
            RedirectUri = options.RedirectUri,
            Browser = options.Browser
        });
    }

    public async Task<LoginResult> LoginAsync()
    {
        return await oidcClient.LoginAsync();
    }

    public async Task<LoginResult> SignupAsync()
    {
        var requestUrl = new LoginRequest();
        requestUrl.FrontChannelExtraParameters.Add("prompt", "login");
        requestUrl.FrontChannelExtraParameters.Add("screen_hint", "signup");

        return await oidcClient.LoginAsync(requestUrl);
    }

    public async Task<BrowserResult> LogoutAsync()
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
