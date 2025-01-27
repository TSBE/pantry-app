using CommunityToolkit.Mvvm.ComponentModel;

namespace Pantry.Mobile.Core.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty] private string errorMessage = string.Empty;
    }
}
