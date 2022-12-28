using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Extensions;
using Pantry.Mobile.Core.Infrastructure.Helpers;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;

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

    [ObservableProperty]
    public string barcode = string.Empty;

    public ObservableRangeCollection<InvitationModel> Invitations { get; set; } = new();

    [RelayCommand]
    public async Task Init()
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
    public async Task Delete(InvitationModel invitation)
    {
        if (invitation == null)
            return;

        await _pantryClientApiService.DeclineInvitationAsync(invitation.FriendsCode);
        await Load();
    }

    [RelayCommand]
    public async Task Refresh()
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
    public async Task Load()
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
    public async Task Add()
    {
        ErrorMessage = string.Empty;
        await _navigation.GoToAsync($"{PageConstants.SCANNER_PAGE}");
    }

    [RelayCommand]
    public async Task CreateInvitation(string friendsCode)
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
