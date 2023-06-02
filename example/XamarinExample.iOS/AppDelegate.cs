using System;
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
                ExponeaLinkHandler.Instance.HandleCampaignClick(userActivity.WebPageUrl);
                return userActivity.WebPageUrl.Host == "old.panaxeo.com";
            }

            return false;
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            ExponeaLinkHandler.Instance.HandleCampaignClick(url);
            return url.Host == "old.panaxeo.com";
        }
    }
}
