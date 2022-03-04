using System;
using System.Collections.Generic;
using Android.App;
using Exponea.Android;
using Huawei.Hms.Push;

namespace XamarinExample.Droid
{
    [Service(Name = "XamarinExample.Droid.ExampleHuaweiMessageService", Exported = false)]
    [IntentFilter(new[] { "com.huawei.push.action.MESSAGING_EVENT" })]
    public class ExampleHuaweiMessageService : HmsMessageService
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
            if (!ExponeaNotificationHandler.Instance.HandleRemoteMessage(ApplicationContext, message.DataOfMap, notificationManager, true))
            {
                // push notification is from another push provider
            }
            Console.WriteLine("OnMessageReceived was called.");
        }

        public override void OnNewToken(string token)
        {
            ExponeaNotificationHandler.Instance.HandleNewHmsToken(ApplicationContext, token);
        }
    }
}
