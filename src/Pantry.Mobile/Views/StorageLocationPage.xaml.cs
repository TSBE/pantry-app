using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Views;

public partial class StorageLocationPage : ContentPage
{
    public StorageLocationPage(StorageLocationViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
