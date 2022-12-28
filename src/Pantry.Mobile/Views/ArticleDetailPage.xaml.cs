using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Views;

public partial class ArticleDetailPage : ContentPage
{
    public ArticleDetailPage(ArticleDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
