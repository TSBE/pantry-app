﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Pantry.Mobile.Core.Infrastructure;

namespace Pantry.Mobile;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTask, Exported = true)]
[IntentFilter(new[] { Android.Content.Intent.ActionView },
              Categories = new[] {
                Android.Content.Intent.CategoryDefault,
                Android.Content.Intent.CategoryBrowsable
              },
              DataScheme = AppConstants.AUTH0_CALLBACK_SCHEME)]
public class WebAuthenticationCallbackActivity : Pantry.Mobile.WebAuthenticator.WebAuthenticatorCallbackActivity
{
}
