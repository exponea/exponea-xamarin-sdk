using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Firebase.Messaging;
using Google.Gson.Internal;

namespace Exponea.Android
{
    public class ExponeaNotificationHandler
    {

       private readonly global::ExponeaSdk.Exponea _exponea = global::ExponeaSdk.Exponea.Instance;


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

        public bool HandleRemoteMessage(Context applicationContext, RemoteMessage message, NotificationManager manager, bool showNotification)
        {
           return _exponea.HandleRemoteMessage(applicationContext, message, manager, showNotification);
        }

        public void TrackPushToken(string fcmToken)
        {
           _exponea.TrackPushToken(fcmToken);
        }

        public bool IsExponeaNotification(RemoteMessage remoteMessage)
        {
            return _exponea.IsExponeaPushNotification(remoteMessage);
        }

        public void SetNotificationDataCallback(Action<Dictionary<string, object>> action)
        {
            if (action == null)
            {
                _exponea.NotificationDataCallback = null;

            }
            else
            {
                _exponea.NotificationDataCallback = new KotlinCallback<LinkedTreeMap>(map =>
                  {
                      var extras = new Dictionary<string, object>();

                      if (map != null)
                      {
                          foreach (string key in map.KeySet())
                          {
                              extras.Add(key, map.Get(key));

                          }
                      }
                      action.Invoke(extras);
                  });
            }
        }
    }

    internal class KotlinMapCallback : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction1
       
    {
        private readonly Action<object> _callback;

        public KotlinMapCallback(Action<object> callback)
        {
            _callback = callback;
        }

        public Java.Lang.Object Invoke(Java.Lang.Object p0)
        {
            _callback.Invoke(p0);
            return null;
        }
    }


}
