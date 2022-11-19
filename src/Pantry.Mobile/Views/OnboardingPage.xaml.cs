using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Views;

public partial class OnboardingPage : ContentPage
{
    public OnboardingPage(OnboardingViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
