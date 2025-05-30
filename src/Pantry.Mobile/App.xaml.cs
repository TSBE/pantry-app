﻿using Microsoft.AppCenter;
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
    }
    
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }

    protected override void OnStart()
    {
        if (VersionTracking.IsFirstLaunchEver)
        {
            // clear left overs
            SecureStorage.RemoveAll();
        }

        var initTask = InitAsync();
        initTask.ContinueWith(async (task) =>
        {
            var pageUrl = await task;
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                // Choose navigation depending on init
                await _navigation.GoToAsync(pageUrl, false);
            });
        });

        base.OnStart();
    }

    private async Task<string> InitAsync()
    {
        return await _navigation.GetNextStartupPage(CancellationToken.None);
    }
}
