using System;
using Android.App;
using Firebase.Messaging;

namespace XamarinExample.Droid
{
   //todo: move this to SDK if possible
    public class ExampleFirebaseMessageService : FirebaseMessagingService
    {
        private static ExponeaSdk.Exponea _exponea = global::ExponeaSdk.Exponea.Instance;

        public override void OnMessageReceived(RemoteMessage message)
        { 
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
           if (!_exponea.HandleRemoteMessage(ApplicationContext, message, notificationManager, true)) {
                // push notification is from another push provider
            }
            Console.WriteLine("OnMessageReceived was called.");
        }

        public override void OnNewToken(string token)
        {
            _exponea.TrackPushToken(token);
        }
    }
}
