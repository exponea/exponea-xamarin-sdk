using System;
using System.Threading.Tasks;
using Exponea.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace XamarinExample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : ExponeaFormsAppDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary launchOptions)
        {
            Forms.Init();
            DependencyService.Register<Exponea.IExponeaSdk, Exponea.ExponeaSdk>();
            DependencyService.Register<IPushRegistrationHandler, PushRegistrationHandler>();

            LoadApplication(new App());

            return base.FinishedLaunching(app, launchOptions);
        }

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {

            if (userActivity.ActivityType == "NSUserActivityTypeBrowsingWeb" && userActivity.WebPageUrl != null)
            {
                Util util = new Util();
                //call validation through api, parse the result and pass it into handleDeepLink method
                Task.Run(async() => await util.VerifyUrl(userActivity.WebPageUrl.Host, false))
                .ContinueWith(task => { HandleDeepLink(task.Result, userActivity.WebPageUrl, application); }, TaskScheduler.FromCurrentSynchronizationContext());

                return userActivity.WebPageUrl.Host == "old.panaxeo.com";
            }

            return false;
        }

        private void HandleDeepLink(bool linkPassedValidation, NSUrl webPageUrl, UIApplication application)
        {
            if (linkPassedValidation) {

                //navigate app where you want, we just display deep link info
                var okAlertController = UIAlertController.Create("DeepLink received", "Deepling received for host: " + webPageUrl.Host, UIAlertControllerStyle.Alert);
                okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                application.KeyWindow.RootViewController.PresentViewController(okAlertController, true, null);

                //track click event with ExponeaSDK
                ExponeaLinkHandler.Instance.HandleCampaignClick(webPageUrl);
            } else
            {
                //link did not pass validation, do not react to deeplink
            }
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            Util util = new Util();
            Task.Run(async() => await util.VerifyUrl(url.Host, true))
                .ContinueWith(task => { HandleDeepLink(task.Result, url, app); }, TaskScheduler.FromCurrentSynchronizationContext());
            return url.Host == "old.panaxeo.com";
        }
    }
}
