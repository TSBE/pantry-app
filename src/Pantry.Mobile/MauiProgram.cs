using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Auth0;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.ViewModels;
using Pantry.Mobile.Interactors;
using Pantry.Mobile.Views;
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
            .UseBarcodeReader()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureEssentials(essentials =>
            {
                essentials.UseVersionTracking();
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Register essentials
        builder.Services.AddSingleton(VersionTracking.Default);
        builder.Services.AddSingleton(Preferences.Default);
        builder.Services.AddSingleton(SecureStorage.Default);
        builder.Services.AddSingleton(Connectivity.Current);

        // Register services
        builder.Services.AddSingleton<INavigationService, ShellNavigationWrapper>();
        builder.Services.AddSingleton<IDialogService, ToolkitDialogService>();
        builder.Services.AddSingleton<ISettingsService, SettingsService>();

        // Register pages
        builder.Services.AddSingleton<OnboardingPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<CreateAccountPage>();
        builder.Services.AddSingleton<HouseholdPage>();
        builder.Services.AddSingleton<StorageLocationPage>();
        builder.Services.AddTransient<ScannerPage>();
        builder.Services.AddTransient<AddStorageLocationPage>();
        builder.Services.AddTransient<AddArticlePage>();
        builder.Services.AddTransient<ArticleDetailPage>();
        builder.Services.AddTransient<ManageInvitationsPage>();

        // Register view models
        builder.Services.AddSingleton<OnboardingViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<CreateAccountViewModel>();
        builder.Services.AddSingleton<HouseholdViewModel>();
        builder.Services.AddSingleton<StorageLocationViewModel>();
        builder.Services.AddTransient<AddStorageLocationViewModel>();
        builder.Services.AddTransient<AddArticleViewModel>();
        builder.Services.AddTransient<ArticleDetailViewModel>();
        builder.Services.AddTransient<ManageInvitationsViewModel>();

        //Register helpers
        builder.Services.AddSingleton<IKeyboardHelper, KeyboardHelper>();
        builder.Services.AddSingleton<IAuth0Client>(new Auth0Client(new()
        {
            Audience = AppConstants.AUTH0_AUDIENCE,
            Domain = AppConstants.AUTH0_DOMAIN,
            ClientId = AppConstants.AUTH0_CLIENT_ID,
            Scope = AppConstants.AUTH0_SCOPES,
            RedirectUri = AppConstants.AUTH0_CALLBACK_URL,
            Browser = new Pantry.Mobile.WebAuthenticator.WebBrowserAuthenticator()
        }));
#if DEBUG
        builder.Services.AddSingleton<IPantryClientApiService>(new DummyPantryClientApiService());
#else
        builder.Services.AddRefreshTokenDelegatingHandler();
        builder.Services.AddRefitClient<IPantryClientApiService>(AppConstants.PANTRY_BASEURL);
#endif
        return builder.Build();
    }
}
