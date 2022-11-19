using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Auth0;
using Pantry.Mobile.Core.ViewModels;
using Pantry.Mobile.Interactors;
using Pantry.Mobile.Views;

namespace Pantry.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Register  services
        builder.Services.AddSingleton<INavigationService, ShellNavigationWrapper>();
        builder.Services.AddSingleton<IDialogService, ToolkitDialogService>();


        // Register pages
        builder.Services.AddSingleton<OnboardingPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<SettingsPage>();

        // Register view models
        builder.Services.AddSingleton<OnboardingViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();

        //Register helpers
        builder.Services.AddSingleton(new Auth0Client(new()
        {
            Domain = AppConstants.AUTH0_DOMAIN,
            ClientId = AppConstants.AUTH0_CLIENT_ID,
            Scope = AppConstants.AUTH0_SCOPES,
            RedirectUri = AppConstants.AUTH0_CALLBACK_URL
        }));

        return builder.Build();
    }
}
