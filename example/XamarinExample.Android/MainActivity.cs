using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;

namespace XamarinExample.Droid
{
    [Activity(Label = "XamarinExample", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "https",
        DataHost = "old.panaxeo.com",
        DataPathPattern = "/exponea/.*",
        AutoVerify = true
    )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (IsViewUrlIntent(Intent, "http") || IsViewUrlIntent(Intent, "exponea"))
            {
                Console.WriteLine(String.Format("Deep link received from {0}, " +
                            "path is {0}", Intent.Data.Host, Intent.Data.Path));

            }

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Forms.DependencyService.Register<Exponea.IExponeaSdk, Exponea.ExponeaSdk>();

            LoadApplication(new App());

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private bool IsViewUrlIntent(Intent intent, String schemePrefix)
        {
            return Intent.ActionView == intent.Action && intent.Data.Scheme.StartsWith(schemePrefix);
        }

    }
}
