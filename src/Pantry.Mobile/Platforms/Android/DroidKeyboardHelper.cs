using System.Runtime.Versioning;
using Android.Content;
using Android.Views.InputMethods;
using Pantry.Mobile.Core.Infrastructure.Abstractions;

namespace Pantry.Mobile;

public class DroidKeyboardHelper : IKeyboardHelper
{
    public DroidKeyboardHelper()
    {
    }

    [SupportedOSPlatform("Android10.0")]
    public void HideKeyboard()
    {
        Context context = Android.App.Application.Context;
        if (context.GetSystemService(Context.InputMethodService) is InputMethodManager inputMethodManager)
        {
            var activity = Platform.CurrentActivity;
            var token = activity?.CurrentFocus?.WindowToken;
            inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
            activity?.Window?.DecorView.ClearFocus();
        }
    }
}
