using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Auth0;

namespace Pantry.Mobile.Core.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;

        private readonly ISettingsService _settingsService;

        private readonly IAuth0Client _auth0Client;

        public LoginViewModel(INavigationService navigation, ISettingsService settingsService, IAuth0Client client)
        {
            _navigation = navigation;
            _settingsService = settingsService;
            _auth0Client = client;
        }

        [RelayCommand]
        private async Task Login()
        {
            try
            {
                ErrorMessage = string.Empty;
                var credentials = await _auth0Client.Login();
                await HandleNextStep(credentials);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        [RelayCommand]
        private async Task Signup()
        {
            try
            {
                ErrorMessage = string.Empty;
                var credentials = await _auth0Client.Signup();
                await HandleNextStep(credentials);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private async Task HandleNextStep(Credentials credentials)
        {
            if (!credentials.HasError)
            {
                await _settingsService.SetCredentials(credentials);
                var nextPage = await _navigation.GetNextStartupPage(CancellationToken.None);
                await _navigation.GoToAsync(nextPage, false);
            }
            else
            {
                ErrorMessage = credentials.Error;
            }
        }
    }
}
