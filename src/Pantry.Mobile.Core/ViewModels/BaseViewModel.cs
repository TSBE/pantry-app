using CommunityToolkit.Mvvm.ComponentModel;

namespace Pantry.Mobile.Core.ViewModels
{
    [INotifyPropertyChanged]
    public abstract partial class BaseViewModel
    {
        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        public string errorMessage = string.Empty;
    }
}
