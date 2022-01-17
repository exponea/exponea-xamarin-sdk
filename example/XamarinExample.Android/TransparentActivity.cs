
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Exponea.Android;

namespace XamarinExample.Droid
{
    [Activity(Label = "TransparentActivity", Theme = "@android:style/Theme.Translucent.NoTitleBar", NoHistory = true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "https",
        DataHost = "old.panaxeo.com",
        DataPathPattern = "/xamarin/.*",
        AutoVerify = true
    )]
    public class TransparentActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Intent.Data != null)
            {
                Util util = new Util();
                //call validation through api, parse the result and pass it into handleDeepLink method
                Task.Run(async () => await util.VerifyUrl(Intent.Data.Path, true))
                .ContinueWith(task => { HandleDeepLink(task.Result, Intent); }, TaskScheduler.FromCurrentSynchronizationContext());

            }
        }

        private void HandleDeepLink(bool linkPassedValidation, Intent intent)
        {
            if (linkPassedValidation)
            {
                //navigate app where you want

                var activity = new Intent(this, typeof(MainActivity));

                activity.PutExtra("link", Intent.Data.Host + Intent.Data.Path);

                StartActivity(activity);

                //track click event with ExponeaSDK
                ExponeaLinkHandler.Instance.HandleCampaignClick(intent, this);
                Finish();
            }
            else
            {
                //link did not pass validation, do not react to deeplink
                Finish();
            }
        }
    }
}
