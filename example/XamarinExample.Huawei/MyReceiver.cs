using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using ExponeaSdk.Models;
using ExponeaSdk;

namespace XamarinExample.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "com.exponea.sdk.action.PUSH_CLICKED" })]
    public class MyReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            // Extract payload data

            NotificationData value = (NotificationData)intent.GetParcelableExtra(ExponeaExtras.ExtraData);

            Console.WriteLine("Push notification attributes:");
            // Process the data if you need to
            foreach (KeyValuePair<string, Java.Lang.Object> entry in value.Attributes)
            {
                Console.WriteLine(entry.Key + ":" + entry.Value);
            }
        }
    }
}
