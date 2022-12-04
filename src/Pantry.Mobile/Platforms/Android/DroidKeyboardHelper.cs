using Android.Content;
using Android.Views.InputMethods;
using Pantry.Mobile.Core.Infrastructure.Abstractions;

namespace Pantry.Mobile;

public class DroidKeyboardHelper : IKeyboardHelper
{
    public DroidKeyboardHelper()
    {
    }

    public void HideKeyboard()
    {
        var context = Android.App.Application.Context;
        var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
        if (inputMethodManager != null)
        {
            var activity = Platform.CurrentActivity;
            var token = activity.CurrentFocus?.WindowToken;
            inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
            activity.Window.DecorView.ClearFocus();
        }
    }
}
