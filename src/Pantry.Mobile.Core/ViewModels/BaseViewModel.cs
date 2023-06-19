using CommunityToolkit.Mvvm.ComponentModel;

namespace Pantry.Mobile.Core.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        public string errorMessage = string.Empty;
    }
}
