using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Pantry.Mobile.Core.Infrastructure;

namespace Pantry.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        AppCenter.Start($"ios={AppConstants.APPCENTER_KEY_IOS};android={AppConstants.APPCENTER_KEY_IOS};", typeof(Analytics), typeof(Crashes));
        MainPage = new AppShell();
    }
}
