namespace Pantry.Mobile.Core.Infrastructure.Auth0;

public class Auth0ClientOptions
{
    public string Domain { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string RedirectUri { get; set; } = AppConstants.AUTH0_CALLBACK_URL;

    public string Scope { get; set; } = AppConstants.AUTH0_SCOPES;

    public required IdentityModel.OidcClient.Browser.IBrowser Browser { get; set; }
}
