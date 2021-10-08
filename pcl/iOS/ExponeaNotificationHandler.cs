#nullable enable

using System;
using Foundation;
using UserNotifications;
using ExponeaSdkIos = ExponeaSdk;

namespace Exponea.iOS
{
    public class ExponeaNotificationHandler
    {

        private readonly ExponeaSdkIos.Exponea _exponea = ExponeaSdkIos.Exponea.Instance;

        private static readonly ExponeaNotificationHandler instance = new ExponeaNotificationHandler();

        static ExponeaNotificationHandler()
        {
        }

        private ExponeaNotificationHandler()
        {
        }

        public static ExponeaNotificationHandler Instance
        {
            get
            {
                return instance;
            }
        }

        public void HandlePushNotificationToken(NSData token)
        {
            _exponea.HandlePushNotificationToken(token);
        }


        public void HandlePushNotificationOpened(UNNotificationResponse response)
        {
            _exponea.HandlePushNotificationOpened(response: response);
        }


        public void HandlePushNotificationOpened(NSDictionary userInfo, string? actionIdentifier = null)
        {
            _exponea.HandlePushNotificationOpened(userInfo: userInfo, actionIdentifier: actionIdentifier);

        }

        public void ProcessNotificationRequest(UNNotificationRequest request, Action<UNNotificationContent> contentHandler, string appGroup)
        {
            _exponea.InitNotificationService(appGroup);
            _exponea.ProcessNotificationRequest(request, contentHandler);
        }

        public void HandlePushNotificationReceived(UNNotification notification, NSExtensionContext? context, UIKit.UIViewController controller)
        {
            _exponea.NotificationReceived(notification, context, controller);
        }

        public void TimeWillExpire()
        {
            _exponea.ServiceExtensionTimeWillExpire();
        }

        public static bool IsExponeaNotification(NSDictionary userInfo)
        {
            return ExponeaSdkIos.Exponea.IsExponeaNotification(userInfo);
        }

        public void TrackPushToken(string token)
        {
            _exponea.TrackPushToken(token);
        }

    }
}
