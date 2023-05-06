using Android.App;
using Android.Content;
using Android.OS;

namespace Pantry.Mobile.WebAuthenticator
{
    public abstract class WebAuthenticatorCallbackActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // start the intermediate activity again with flags to close the custom tabs
            var intent = new Intent(this, typeof(MyWebAuthenticatorIntermediateActivity));
            intent.SetData(Intent.Data);
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

            // finish this activity
            Finish();
        }
    }
}
