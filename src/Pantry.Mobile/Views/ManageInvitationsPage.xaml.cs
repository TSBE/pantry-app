using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Views;

public partial class ManageInvitationsPage : ContentPage
{
    public ManageInvitationsPage(ManageInvitationsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
