using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Views;

namespace Pantry.Mobile;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute(PageConstants.ScannerPage, typeof(ScannerPage));
        Routing.RegisterRoute(PageConstants.AddStorageLocationPage, typeof(AddStorageLocationPage));
        Routing.RegisterRoute(PageConstants.AddArticlePage, typeof(AddArticlePage));
        Routing.RegisterRoute(PageConstants.ArticleDetailPage, typeof(ArticleDetailPage));
        Routing.RegisterRoute(PageConstants.ManageInvitationsPage, typeof(ManageInvitationsPage));
    }
}
