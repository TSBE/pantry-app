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
        Routing.RegisterRoute(nameof(ScannerPage), typeof(ScannerPage));
    }
}
