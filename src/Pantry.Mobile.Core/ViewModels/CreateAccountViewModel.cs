﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;

namespace Pantry.Mobile.Core.ViewModels
{
    public partial class CreateAccountViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;

        private readonly IPantryClientApiService _pantryClientApiService;

        public CreateAccountViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService)
        {
            _navigation = navigation;
            _pantryClientApiService = pantryClientApiService;
        }

        [ObservableProperty] private string firstName = string.Empty;

        [ObservableProperty] private string lastName = string.Empty;

        [RelayCommand]
        private async Task CreateAccount()
        {
            try
            {
                ErrorMessage = string.Empty;
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                {
                    ErrorMessage = "Input invalid";
                    return;
                }

                await _pantryClientApiService.CreateAccountAsync(new AccountRequest() { FirstName = FirstName, LastName = LastName });

                var nextPage = await _navigation.GetNextStartupPage(CancellationToken.None);
                await _navigation.GoToAsync(nextPage, false);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
