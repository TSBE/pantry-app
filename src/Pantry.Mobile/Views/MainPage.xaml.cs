using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
