namespace Pantry.Mobile.Core.Infrastructure.Auth0;

public class Credentials
{
    public string AccessToken { get; set; } = string.Empty;
    public string IdentityToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTimeOffset AccessTokenExpiration { get; set; }
    public string Error { get; set; } = string.Empty;
    public bool HasError => !string.IsNullOrEmpty(Error);
}
