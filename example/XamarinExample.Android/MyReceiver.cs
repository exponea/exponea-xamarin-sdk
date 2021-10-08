using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using ExponeaSdk.Services;
using ExponeaSdk.Models;

namespace XamarinExample.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "com.exponea.sdk.action.PUSH_CLICKED" })]
    public class MyReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            // Extract payload data

            NotificationData value = (NotificationData)intent.GetParcelableExtra(ExponeaPushReceiver.ExtraData);

            Console.WriteLine("Push notification attributes:");
            // Process the data if you need to
            foreach (KeyValuePair<string, Java.Lang.Object> entry in value.Attributes)
            {
                Console.WriteLine(entry.Key + ":" + entry.Value);
            }

            // Start an activity
            var newIntent = new Intent(context, typeof(MainActivity));
            newIntent.AddFlags(ActivityFlags.NewTask);
            context.StartActivity(newIntent);
        }
    }
}
