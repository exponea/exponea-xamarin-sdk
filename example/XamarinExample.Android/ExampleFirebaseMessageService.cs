using System;
using System.Collections.Generic;
using Android.App;
using Exponea.Android;
using Firebase.Messaging;

namespace XamarinExample.Droid
{
    // Uncoment if you want to use custom message service
    //[Service(Name = "XamarinExample.Droid.ExampleFirebaseMessageService")]
    //[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    //[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class ExampleFirebaseMessageService : FirebaseMessagingService
    {

        public override void OnMessageReceived(RemoteMessage message)
        { 
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            Action<Dictionary<string, object>> action = (dictionary) =>
            {
                foreach (KeyValuePair<string, object> entry in dictionary)
                {
                    Console.WriteLine(String.Format("Push Extra: {0} : {1} ", entry.Key, entry.Value));
                }
            };

            ExponeaNotificationHandler.Instance.SetNotificationDataCallback(action);
            if (!ExponeaNotificationHandler.Instance.HandleRemoteMessage(ApplicationContext, message, notificationManager, true)) {
                // push notification is from another push provider
            }
            Console.WriteLine("OnMessageReceived was called.");
        }

        public override void OnNewToken(string token)
        {
            ExponeaNotificationHandler.Instance.TrackPushToken(token);
        }
    }
}
