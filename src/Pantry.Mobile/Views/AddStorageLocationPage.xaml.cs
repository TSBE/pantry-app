using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Views;

public partial class AddStorageLocationPage : ContentPage
{
    public AddStorageLocationPage(AddStorageLocationViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
