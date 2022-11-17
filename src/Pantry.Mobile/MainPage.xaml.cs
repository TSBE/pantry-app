using Microsoft.AppCenter.Crashes;

namespace Pantry;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        if (count == 5)
        {
            Crashes.GenerateTestCrash();
        }

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}


