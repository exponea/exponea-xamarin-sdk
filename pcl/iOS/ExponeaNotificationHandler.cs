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
