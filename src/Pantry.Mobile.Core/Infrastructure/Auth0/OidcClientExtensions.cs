using Duende.IdentityModel.OidcClient.Results;
using Duende.IdentityModel.OidcClient;

namespace Pantry.Mobile.Core.Infrastructure.Auth0;

public static class OidcClientExtensions
{
    public static Credentials ToCredentials(this LoginResult loginResult)
        => new()
        {
            AccessToken = loginResult.AccessToken,
            IdentityToken = loginResult.IdentityToken,
            RefreshToken = loginResult.RefreshToken,
            AccessTokenExpiration = loginResult.AccessTokenExpiration
        };

    public static Credentials ToCredentials(this RefreshTokenResult refreshTokenResult, string refreshToken)
        => new()
        {
            AccessToken = refreshTokenResult.AccessToken,
            IdentityToken = refreshTokenResult.IdentityToken,
            RefreshToken = !string.IsNullOrWhiteSpace(refreshTokenResult.RefreshToken) ? refreshTokenResult.RefreshToken : refreshToken,
            AccessTokenExpiration = refreshTokenResult.AccessTokenExpiration
        };
}
