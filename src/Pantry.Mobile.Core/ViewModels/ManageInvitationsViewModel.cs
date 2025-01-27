using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Extensions;
using Pantry.Mobile.Core.Infrastructure.Helpers;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;
using ZXing.Net.Maui;

namespace Pantry.Mobile.Core.ViewModels;

[QueryProperty(nameof(Barcode), nameof(Barcode))]
public partial class ManageInvitationsViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;

    private readonly IPantryClientApiService _pantryClientApiService;

    public ManageInvitationsViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService)
    {
        _navigation = navigation;
        _pantryClientApiService = pantryClientApiService;
    }

    [ObservableProperty] private string barcode = string.Empty;

    public ObservableRangeCollection<InvitationModel> Invitations { get; set; } = [];

    [RelayCommand]
    private async Task Init()
    {
        try
        {
            await Load();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    [RelayCommand]
    private async Task Delete(InvitationModel invitation)
    {
        await _pantryClientApiService.DeclineInvitationAsync(invitation.FriendsCode);
        await Load();
    }

    [RelayCommand]
    private async Task Refresh()
    {
        try
        {
            ErrorMessage = string.Empty;

            await Load();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task Load()
    {
        var invitationListResponse = await _pantryClientApiService.GetInvitationAsync();

        if (invitationListResponse?.Invitations is null)
        {
            return;
        }

        var invitations = (from item in invitationListResponse?.Invitations select item.ToInvitationModel()).ToList();
        Invitations.Clear();
        Invitations.AddRange(invitations);
    }

    [RelayCommand]
    private async Task Add()
    {
        ErrorMessage = string.Empty;
        await _navigation.GoToAsync($"{PageConstants.ScannerPage}?ActiveBarcodeFormat={BarcodeFormat.QrCode}");
    }

    [RelayCommand]
    private async Task CreateInvitation(string friendsCode)
    {
        try
        {
            ErrorMessage = string.Empty;

            if (Guid.TryParse(friendsCode, out var friendsCodeGuid))
            {
                await _pantryClientApiService.CreateInvitationAsync(new InvitationRequest { FriendsCode = friendsCodeGuid });
                await Load();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }

    partial void OnBarcodeChanged(string value)
    {
        CreateInvitationCommand?.Execute(value);
    }
}
