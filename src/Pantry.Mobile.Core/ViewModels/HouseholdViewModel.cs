using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
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

        private readonly string createHouseholdText = "New";

        private readonly string joinHouseholdText = "Join";

        private readonly string cancelText = "Cancel";

        private readonly AsyncRetryPolicy<InvitationListResponse> retryWithDelay = Policy.HandleResult<InvitationListResponse>(x => { return x?.Invitations?.FirstOrDefault() is null; }).WaitAndRetryForeverAsync(x => new TimeSpan(0, 0, 5));

        private CancellationTokenSource pollingCancellation = new();

        public HouseholdViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService)
        {
            _navigation = navigation;
            _pantryClientApiService = pantryClientApiService;

            toggleCreateText = createHouseholdText;
            toggleJoinText = joinHouseholdText;
        }

        [ObservableProperty]
        public string errorMessage = string.Empty;

        [ObservableProperty]
        public string name = string.Empty;

        [ObservableProperty]
        public string friendsCode = string.Empty;

        [ObservableProperty]
        public ObservableCollection<InvitationModel> invitations = new();

        [ObservableProperty]
        public bool isPolling = false;

        [ObservableProperty]
        public bool isCreateVisible = false;

        [ObservableProperty]
        public bool isJoinVisible = false;

        [ObservableProperty]
        public string toggleCreateText;

        [ObservableProperty]
        public string toggleJoinText;

        [RelayCommand]
        public async Task Init()
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
        public void ToggleCreate()
        {
            IsCreateVisible = !IsCreateVisible;
            IsJoinVisible = false;
            SetButtonText();
        }

        [RelayCommand]
        public void ToggleJoin()
        {
            IsCreateVisible = false;
            IsJoinVisible = !IsJoinVisible;
            SetButtonText();
            if (IsJoinVisible)
            {
                pollingCancellation = new CancellationTokenSource();
                IsPolling = true;
                retryWithDelay.ExecuteAsync(ct => CheckInvitation(ct), pollingCancellation.Token).ContinueWith((x) => { IsPolling = false; });
            }
            else
            {
                IsPolling = false;
                Invitations.Clear();
                pollingCancellation.Cancel();
            }
        }

        [RelayCommand]
        public async Task CreateHousehold()
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

                var nextPage = await _navigation.GetNextStartupPage();
                await _navigation.GoToAsync(nextPage, false);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        [RelayCommand]
        public async Task AcceptInvitation(InvitationModel model)
        {
            try
            {
                await _pantryClientApiService.AcceptInvitationAsync(model.FriendsCode);

                pollingCancellation.Cancel();
                var nextPage = await _navigation.GetNextStartupPage();
                await _navigation.GoToAsync(nextPage, false);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        [RelayCommand]
        public async Task DeclineInvitation(InvitationModel model)
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
            ToggleCreateText = IsCreateVisible ? cancelText : createHouseholdText;
            ToggleJoinText = IsJoinVisible ? cancelText : joinHouseholdText;
        }

        private async Task<InvitationListResponse> CheckInvitation(CancellationToken ct)
        {
            var invitationList = await _pantryClientApiService.GetInvitationAsync(ct);
            if (invitationList?.Invitations is not null)
            {
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
            }
            return invitationList ?? new InvitationListResponse();
        }
    }
}
