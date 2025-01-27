using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Extensions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;

    private readonly IPantryClientApiService _pantryClientApiService;

    private readonly ISettingsService _settingsService;

    private readonly IAuth0Client _auth0Client;

    public SettingsViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService, ISettingsService settingsService, IAuth0Client client)
    {
        _navigation = navigation;
        _pantryClientApiService = pantryClientApiService;
        _settingsService = settingsService;
        _auth0Client = client;
    }

    [ObservableProperty] private AccountModel? account;

    [RelayCommand]
    private async Task Init()
    {
        try
        {
            IsBusy = true;
            var accountResponse = await _pantryClientApiService.GetAccountAsync();
            Account = accountResponse.ToAccountModel();
        }
        catch (Exception)
        {
            // ignored
        }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private async Task ManageInvitations()
    {
        try
        {
            IsBusy = true;
            await _navigation.GoToAsync($"{PageConstants.ManageInvitationsPage}");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private async Task Logout()
    {
        try
        {
            IsBusy = true;
            await _auth0Client.Logout();
            await _settingsService.DeleteCredentials();
            var targetPage = await _navigation.GetNextStartupPage(CancellationToken.None);
            await _navigation.GoToAsync(targetPage);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally { IsBusy = false; }
    }
}
