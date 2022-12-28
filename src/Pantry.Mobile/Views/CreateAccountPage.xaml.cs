using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Views;

public partial class CreateAccountPage : ContentPage
{
    public CreateAccountPage(CreateAccountViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
