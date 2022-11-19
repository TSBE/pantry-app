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

        private readonly Auth0Client _auth0Client;

        public LoginViewModel(INavigationService navigation, IDialogService dialogService, Auth0Client client)
        {
            _navigation = navigation;
            _dialogService = dialogService;
            _auth0Client = client;
        }

        [RelayCommand]
        public async Task Login()
        {
            try
            {
                var loginResult = await _auth0Client.LoginAsync();
                if (!loginResult.IsError)
                {
                    await _dialogService.ShowMessage(loginResult?.User.Identity.Name ?? string.Empty);
                    await _navigation.GoToAsync($"//{PageConstants.TABBAR_PAGE}", false);
                }
                else
                {
                    await _dialogService.ShowMessage(loginResult.ErrorDescription);
                }
            }
            catch (InvalidOperationException iex)
            {
            }
            catch (Exception ex)
            {
            }
        }

        [RelayCommand]
        public async Task Signup()
        {
            try
            {
                var loginResult = await _auth0Client.SignupAsync();
                if (!loginResult.IsError)
                {
                    await _dialogService.ShowMessage(loginResult?.User.Identity.Name ?? string.Empty);
                }
                else
                {
                    await _dialogService.ShowMessage(loginResult.ErrorDescription);
                }
            }
            catch (InvalidOperationException iex)
            {
            }
            catch (Exception ex)
            {
            }
        }
    }
}
