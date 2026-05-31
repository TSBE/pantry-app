using BarcodeScanning;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using ZXing.Net.Maui.Controls;

namespace Pantry.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
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
                essentials.UseVersionTracking()
                    .AddAppAction("mock", "Mock Mode", icon: "dotnet_bot")
                    .OnAppAction(HandleAppActions);
            })
#if DEBUG
            .Logging.AddDebug()
#endif
            .Services.RegisterEssentials()
            .RegisterServices()
            .RegisterPages()
            .RegisterViewModels()
            .RegisterHelpers();
        
        return builder.Build();
    }

    private static void HandleAppActions(AppAction appAction)
    {
        var settingsService = IPlatformApplication.Current?.Services.GetRequiredService<ISettingsService>();
        if (appAction.Id.Equals("mock") && settingsService is not null)
        {
            settingsService.IsMockModeEnabled = !settingsService.IsMockModeEnabled;
        }
    }
}
