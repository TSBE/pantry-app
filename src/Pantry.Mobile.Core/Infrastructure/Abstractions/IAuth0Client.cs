using IdentityModel.OidcClient.Browser;
using IdentityModel.OidcClient.Results;
using Pantry.Mobile.Core.Infrastructure.Auth0;

namespace Pantry.Mobile.Core.Infrastructure.Abstractions
{
    public interface IAuth0Client
    {
        public Task<Credentials> Login();

        public Task<Credentials> Signup();

        public Task<Credentials> RefreshToken(string refreshToken, CancellationToken cancellationToken);

        public Task<UserInfoResult> GetUserInfo(string accessToken);

        public Task<BrowserResult> Logout();
    }
}
