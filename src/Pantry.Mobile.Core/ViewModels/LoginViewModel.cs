using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Auth0;

namespace Pantry.Mobile.Core.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;

        private readonly IDialogService _dialogService;

        private readonly ISettingsService _settingsService;

        private readonly Auth0Client _auth0Client;

        public LoginViewModel(INavigationService navigation, IDialogService dialogService, ISettingsService settingsService, Auth0Client client)
        {
            _navigation = navigation;
            _dialogService = dialogService;
            _settingsService = settingsService;
            _auth0Client = client;
        }

        [ObservableProperty]
        public string errorMessage = string.Empty;

        [RelayCommand]
        public async Task Login()
        {
            try
            {
                var credentials = await _auth0Client.LoginAsync();
                if (!credentials.HasError)
                {
                    await _settingsService.SetCredentials(credentials);
                    await _dialogService.ShowMessage(credentials.AccessToken);
                    await _navigation.GoToAsync($"//{PageConstants.TABBAR_PAGE}", false);
                }
                else
                {
                    ErrorMessage = credentials.Error;
                }
            }
            catch (Exception)
            {
            }
        }

        [RelayCommand]
        public async Task Signup()
        {
            try
            {
                var credentials = await _auth0Client.SignupAsync();
                if (!credentials.HasError)
                {
                    await _settingsService.SetCredentials(credentials);
                    await _dialogService.ShowMessage(credentials.AccessToken);
                    await _navigation.GoToAsync($"//{PageConstants.TABBAR_PAGE}", false);
                }
                else
                {
                    ErrorMessage = credentials.Error;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
