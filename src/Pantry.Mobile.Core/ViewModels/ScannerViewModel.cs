using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure.Abstractions;

namespace Pantry.Mobile.Core.ViewModels
{
    public partial class ScannerViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;

        private readonly IDialogService _dialogService;

        private readonly ISettingsService _settingsService;

        public ScannerViewModel(INavigationService navigation, IDialogService dialogService, ISettingsService settingsService)
        {
            _navigation = navigation;
            _dialogService = dialogService;
            _settingsService = settingsService;
        }
    }
}
