using Android.App;
using Android.Content.PM;
using Pantry.Mobile.Core.Infrastructure;

namespace Pantry.Mobile;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTask, Exported = true)]
[IntentFilter([Android.Content.Intent.ActionView],
              Categories =
              [
                  Android.Content.Intent.CategoryDefault,
                  Android.Content.Intent.CategoryBrowsable
              ],
              DataScheme = AppConstants.AUTH0_CALLBACK_SCHEME)]
public class WebAuthenticationCallbackActivity : WebAuthenticatorCallbackActivity;
