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
        //Routing.RegisterRoute(PageConstants.CREATEACCOUNT_PAGE, typeof(CreateAccountPage));
        //Routing.RegisterRoute(PageConstants.CREATEHOUSEHOLD_PAGE, typeof(HouseholdPage));
    }
}
