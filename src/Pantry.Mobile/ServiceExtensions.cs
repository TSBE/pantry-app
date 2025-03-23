using Pantry.Mobile.Authenticator;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Auth0;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.ViewModels;
using Pantry.Mobile.Interactors;
using Pantry.Mobile.Views;

namespace Pantry.Mobile;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterEssentials(this IServiceCollection service)
    {
        return service.AddSingleton(VersionTracking.Default)
            .AddSingleton(Preferences.Default)
            .AddSingleton(SecureStorage.Default)
            .AddSingleton(Connectivity.Current)
            .AddSingleton(DeviceInfo.Current);
    }
        
    public static IServiceCollection RegisterServices(this IServiceCollection service)
    {
        return service.AddSingleton<INavigationService, ShellNavigationWrapper>()
#if DEBUG
            .AddSingleton<IPantryClientApiService>(new DummyPantryClientApiService())
#else
            .AddRefitClient<IPantryClientApiService>(AppConstants.PANTRY_BASEURL)
#endif
            .AddSingleton<IDialogService, ToolkitDialogService>()
            .AddSingleton<ISettingsService, SettingsService>();
    }
        
    public static IServiceCollection RegisterPages(this IServiceCollection service)
    {
        return service.AddSingleton<OnboardingPage>()
            .AddSingleton<LoginPage>()
            .AddSingleton<MainPage>()
            .AddSingleton<SettingsPage>()
            .AddSingleton<CreateAccountPage>()
            .AddSingleton<HouseholdPage>()
            .AddSingleton<StorageLocationPage>()
            .AddTransient<ScannerPage>()
            .AddTransient<AddStorageLocationPage>()
            .AddTransient<AddArticlePage>()
            .AddTransient<ArticleDetailPage>()
            .AddTransient<ManageInvitationsPage>();
    }
        
    public static IServiceCollection RegisterViewModels(this IServiceCollection service)
    {
        return service.AddSingleton<OnboardingViewModel>()
            .AddSingleton<LoginViewModel>()
            .AddSingleton<MainViewModel>()
            .AddSingleton<SettingsViewModel>()
            .AddSingleton<CreateAccountViewModel>()
            .AddSingleton<HouseholdViewModel>()
            .AddSingleton<StorageLocationViewModel>()
            .AddTransient<ScannerViewModel>()
            .AddTransient<AddStorageLocationViewModel>()
            .AddTransient<AddArticleViewModel>()
            .AddTransient<ArticleDetailViewModel>()
            .AddTransient<ManageInvitationsViewModel>();
    }
        
    public static IServiceCollection RegisterHelpers(this IServiceCollection service)
    {
        return service.AddSingleton<IKeyboardHelper, KeyboardHelper>()
            .AddRefreshTokenDelegatingHandler()
            .AddSingleton<IAuth0Client>(new Auth0Client(new Auth0ClientOptions
            {
                Audience = AppConstants.AUTH0_AUDIENCE,
                Domain = AppConstants.AUTH0_DOMAIN,
                ClientId = AppConstants.AUTH0_CLIENT_ID,
                Scope = AppConstants.AUTH0_SCOPES,
                RedirectUri = AppConstants.AUTH0_CALLBACK_URL,
                Browser = new WebBrowserAuthenticator()
            }));
    }
}
