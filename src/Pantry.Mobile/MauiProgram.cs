using BarcodeScanning;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace Pantry.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var isMockedMode = false;
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitCore()
            .UseBarcodeScanning()
            .UseBarcodeReader()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureEssentials(essentials =>
            {
                essentials.UseVersionTracking();

                essentials.AddAppAction("mock", "Mock Mode", "settings");

                // Handle what happens when an action is clicked
                essentials.OnAppAction(action =>
                {
                    if (action.Id == "mock")
                    {
                        isMockedMode = true;
                    }
                });
            })
#if DEBUG
            .Logging.AddDebug()
#endif
            .Services.RegisterEssentials()
            .RegisterServices(isMockedMode)
            .RegisterPages()
            .RegisterViewModels()
            .RegisterHelpers();
        
        return builder.Build();
    }
}
