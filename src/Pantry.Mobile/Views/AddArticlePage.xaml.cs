using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Views;

public partial class AddArticlePage : ContentPage
{
    public AddArticlePage(AddArticleViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
