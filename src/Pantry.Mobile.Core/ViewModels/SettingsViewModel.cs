using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;

namespace Pantry.Mobile.Core.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;

    public SettingsViewModel(INavigationService navigation)
    {
        _navigation = navigation;
    }
}
