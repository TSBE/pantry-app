using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Pantry;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        AppCenter.Start(
               "ios=10c794f1-e97e-424a-bb13-3938fce0a316;" +
               "android=491d7d80-a368-4675-80a2-f87eea2d7cec;",
               typeof(Analytics), typeof(Crashes));

        MainPage = new AppShell();
    }
}

