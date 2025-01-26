using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Enums;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;
using Polly;
using Polly.Retry;

namespace Pantry.Mobile.Core.ViewModels
{
    public partial class HouseholdViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;

        private readonly IPantryClientApiService _pantryClientApiService;

        private const string CreateHouseholdText = "New";

        private const string JoinHouseholdText = "Join";

        private const string CancelText = "Cancel";

        private readonly AsyncRetryPolicy<InvitationListResponse> _retryWithDelay = Policy.HandleResult<InvitationListResponse>(x => { return x?.Invitations?.FirstOrDefault() is null; }).WaitAndRetryForeverAsync(x => new TimeSpan(0, 0, 5));

        private CancellationTokenSource _pollingCancellation = new();

        public HouseholdViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService)
        {
            _navigation = navigation;
            _pantryClientApiService = pantryClientApiService;

            toggleCreateText = CreateHouseholdText;
            toggleJoinText = JoinHouseholdText;
        }

        [ObservableProperty] private string name = string.Empty;

        [ObservableProperty] private string friendsCode = string.Empty;

        [ObservableProperty] private ObservableCollection<InvitationModel> invitations = [];

        [ObservableProperty] private bool isPolling;

        [ObservableProperty] private bool isCreateVisible;

        [ObservableProperty] private bool isJoinVisible;

        [ObservableProperty] private string toggleCreateText;

        [ObservableProperty] private string toggleJoinText;

        [RelayCommand]
        private async Task Init()
        {
            try
            {
                var account = await _pantryClientApiService.GetAccountAsync();
                FriendsCode = account.FriendsCode.ToString();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        [RelayCommand]
        private void ToggleCreate()
        {
            IsCreateVisible = !IsCreateVisible;
            IsJoinVisible = false;
            SetButtonText();
        }

        [RelayCommand]
        private void ToggleJoin()
        {
            IsCreateVisible = false;
            IsJoinVisible = !IsJoinVisible;
            SetButtonText();
            if (IsJoinVisible)
            {
                _pollingCancellation = new CancellationTokenSource();
                IsPolling = true;
                _retryWithDelay.ExecuteAsync(CheckInvitation, _pollingCancellation.Token).ContinueWith((x) => { IsPolling = false; });
            }
            else
            {
                IsPolling = false;
                Invitations.Clear();
                _pollingCancellation.Cancel();
            }
        }

        [RelayCommand]
        private async Task CreateHousehold()
        {
            try
            {
                ErrorMessage = string.Empty;
                if (string.IsNullOrEmpty(Name))
                {
                    ErrorMessage = "Input invalid";
                    return;
                }

                await _pantryClientApiService.CreateHouseholdAsync(new HouseholdRequest { Name = Name, SubscriptionType = SubscriptionType.FREE });

                var nextPage = await _navigation.GetNextStartupPage(CancellationToken.None);
                await _navigation.GoToAsync(nextPage, false);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        [RelayCommand]
        private async Task AcceptInvitation(InvitationModel model)
        {
            try
            {
                await _pantryClientApiService.AcceptInvitationAsync(model.FriendsCode);

                await _pollingCancellation.CancelAsync();
                var nextPage = await _navigation.GetNextStartupPage(CancellationToken.None);
                await _navigation.GoToAsync(nextPage, false);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        [RelayCommand]
        private async Task DeclineInvitation(InvitationModel model)
        {
            try
            {
                await _pantryClientApiService.DeclineInvitationAsync(model.FriendsCode);

                IsCreateVisible = false;
                IsJoinVisible = false;
                SetButtonText();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private void SetButtonText()
        {
            ErrorMessage = string.Empty;
            ToggleCreateText = IsCreateVisible ? CancelText : CreateHouseholdText;
            ToggleJoinText = IsJoinVisible ? CancelText : JoinHouseholdText;
        }

        private async Task<InvitationListResponse> CheckInvitation(CancellationToken ct)
        {
            var invitationList = await _pantryClientApiService.GetInvitationAsync(ct);
            if (invitationList.Invitations is null)
            {
                return invitationList;
            }

            Invitations.Clear();
            foreach (var i in invitationList.Invitations)
            {
                Invitations.Add(new InvitationModel
                {
                    FriendsCode = i.FriendsCode,
                    CreatorName = i.CreatorName,
                    ValidUntilDate = i.ValidUntilDate,
                    HouseholdName = i.HouseholdName
                });
            }
            return invitationList;
        }
    }
}
