using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.ViewModels
{
    public partial class OnboardingViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;

        private readonly ISettingsService _settingsService;

        public OnboardingViewModel(INavigationService navigation, ISettingsService settingsService)
        {
            _navigation = navigation;
            _settingsService = settingsService;

            introScreens =
            [
                new IntroScreenModel
                {
                    Title = "Food waste",
                    Description = "With this app, you manage your supplies and avoid food waste.",
                    Image = "food_waste"
                },

                new IntroScreenModel
                {
                    Title = "Check list",
                    Description = "Create a listing of your supplies, and the expiration date.",
                    Image = "check_list"
                },

                new IntroScreenModel
                {
                    Title = "Storage locations",
                    Description = "Manage the various storage locations.",
                    Image = "location"
                },

                new IntroScreenModel
                {
                    Title = "Reminder",
                    Description = "Filter the food by expiration date or let yourself be reminded.",
                    Image = "reminder"
                },

                new IntroScreenModel
                {
                    Title = "Family",
                    Description = "Manage the supplies together with the family.",
                    Image = "family"
                },

                new IntroScreenModel
                {
                    Title = "Login",
                    Description = "With the login, only you and members of the household have access.",
                    Image = "user"
                }
            ];
        }

        [ObservableProperty] private string buttonText = "Next";

        [ObservableProperty] private int position;

        [ObservableProperty] private ObservableCollection<IntroScreenModel> introScreens;

        [RelayCommand]
        private void PositionChanged(int newPositon)
        {
            ButtonText = newPositon >= IntroScreens.Count - 1 ? "Start" : "Next";
        }

        [RelayCommand]
        private async Task Init()
        {
            var onboardingHasBeenFinished = await _settingsService.GetOnboardingHasBeenFinished();
            if (onboardingHasBeenFinished)
            {
                await _navigation.GoToAsync($"//{PageConstants.LoginPage}", false);
            }
        }

        [RelayCommand]
        private async Task Next()
        {
            if (Position >= IntroScreens.Count - 1)
            {
                await _settingsService.SetOnboardingHasBeenFinished(true);
                await _navigation.GoToAsync($"//{PageConstants.LoginPage}", false);
            }
            else
            {
                Position++;
            }
        }
    }
}
