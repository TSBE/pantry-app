using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Views;

namespace Pantry.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute(PageConstants.SCANNER_PAGE, typeof(ScannerPage));
        Routing.RegisterRoute(PageConstants.ADD_STORAGE_LOCATION_PAGE, typeof(AddStorageLocationPage));
        Routing.RegisterRoute(PageConstants.ADD_ARTICLE_PAGE, typeof(AddArticlePage));
        Routing.RegisterRoute(PageConstants.ARTICLE_DETAIL_PAGE, typeof(ArticleDetailPage));
        Routing.RegisterRoute(PageConstants.MANAGE_INVITATIONS_PAGE, typeof(ManageInvitationsPage));
    }
}
