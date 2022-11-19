using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure.Abstractions;

namespace Pantry.Mobile.Core.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;
    private int _count = 0;

    public MainViewModel(INavigationService navigation)
    {
        _navigation = navigation;
    }

    [ObservableProperty]
    private string countButtonText = "Click me";

    [RelayCommand]
    private void Counter()
    {
        _count++;
        CountButtonText = _count == 1 ? $"Clicked {_count} time" : $"Clicked {_count} times";
    }
}
