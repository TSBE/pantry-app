using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Views;

namespace Pantry.Mobile;

public partial class App : Application
{
    private readonly INavigationService _navigation;
    private readonly ISettingsService _settingsService;

    public App(INavigationService navigationService, ISettingsService settingsService)
    {
        InitializeComponent();
        _navigation = navigationService;
        _settingsService = settingsService;
        AppCenter.Start($"ios={AppConstants.APPCENTER_KEY_IOS};android={AppConstants.APPCENTER_KEY_ANDROID};", typeof(Analytics), typeof(Crashes));
        MainPage = new LoadingPage();
    }

    protected override void OnStart()
    {
        var task = InitAsync();
        task.ContinueWith(async (task) =>
        {
            var pageUrl = await task;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                MainPage = new AppShell();
                // Choose navigation depending on init
                await _navigation.GoToAsync(pageUrl, false);
            });
        });

        base.OnStart();
    }

    private async Task<string> InitAsync()
    {
        var targetPage = $"//{PageConstants.TABBAR_PAGE}";

        var onboardingHasBeenFinished = await _settingsService.GetOnboardingHasBeenFinished();
        if (!onboardingHasBeenFinished)
        {
            targetPage = $"//{PageConstants.LOGIN_PAGE}";
        }

        var loginCredentials = await _settingsService.GetCredentials();
        if (loginCredentials is null || loginCredentials.HasError)
        {
            targetPage = $"//{PageConstants.LOGIN_PAGE}";
        }

        return targetPage;
    }
}
