using System;
using UserNotifications;
using UIKit;
using Foundation;

namespace Exponea.iOS
{
    public class ExponeaFormsAppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary launchOptions)
        {
            UNUserNotificationCenter.Current.Delegate = this;
            return base.FinishedLaunching(app, launchOptions);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            ExponeaNotificationHandler.Instance.HandlePushNotificationToken(deviceToken);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            ExponeaNotificationHandler.Instance.HandlePushNotificationOpened(userInfo);
            completionHandler(UIBackgroundFetchResult.NewData);
        }

        [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
        public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, System.Action completionHandler)
        {
            ExponeaNotificationHandler.Instance.HandlePushNotificationOpened(response);
            completionHandler();
        }
    }
}
