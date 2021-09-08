
## 📣  Android Push Notifications

Exponea allows you to easily create complex scenarios which you can use to send push notifications directly to your customers. The following section explains how to enable push notifications.

## Quick start

For push notifications to work, you'll need to setup a few things:
- create a Firebase project
- integrate Firebase into your application 
- set the Firebase server key in the Exponea web app
- add a broadcast listener for opening push notifications

## Automatic tracking of Push Notifications

In the [Exponea SDK configuration](CONFIG.md), you can enable or disable the automatic push notification tracking by setting the Boolean value to the `AutomaticPushNotification` property and potentially setting up the desired frequency to the `TokenTrackFrequency`(default value is ON_TOKEN_CHANGE).

With `AutomaticPushNotification` enabled, the SDK will correctly display push notifications from Exponea and track a "campaign" event for every delivered/opened push notification with the correct properties.

## Responding to Push notifications

When creating notification using Exponea Web App, you can choose from 3 different actions to be used when tapping the notification or additional buttons on notification.

### 1. Open app
Open app action generates an intent with action `com.exponea.sdk.action.PUSH_CLICKED`. To respond to it, you need to setup a BroadcastReceiver.

``` csharp
using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Com.Exponea.Sdk.Services;
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

            // Process the data if you need to
            
            // Start an activity
            var newIntent = new Intent(context, typeof(MainActivity));
            newIntent.AddFlags(ActivityFlags.NewTask);
            context.StartActivity(newIntent);
        }
    }
}
```

In the BroadcastReceiver you can launch a corresponding activity(e.g. your main activity). Campaign data is included in the intent as `ExponeaPushReceiver.ExtraData`.


### 2. Deep link
Deep link action creates "view" intent that contains the url specified when setting up this action. To respond to this intent, create intent filter on the activity that should handle it. 
``` csharp
[IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "your_scheme",
        DataHost = "your_host",
        DataPathPattern = "your_path_pattern",
        AutoVerify = true
    )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        ...
```

### 3. Open web browser
Open web browser is handled automatically by the SDK and no work is required from the developer to handle it.


## Silent push notifications
Exponea web app allows you to setup silent push notifications, that are not displayed to the user. The SDK tracks `campaign` event when the push notification is delivered, just like for regular notifications. There is no opening for those notifications.

## Manual tracking of Push Notifications
In case you decide to deactivate the automatic push notification, or wish to track push notifications from other providers, you can still track events manually.

#### Track Push Token (FCM)

``` csharp
 _exponea.TrackPushToken("382d4221-3441-44b7-a676-3eb5f515157f");
```

#### Track Delivered Push Notification

``` csharp
 _exponea.Track(new Delivery { ["campaign_id"] = "id" });
```


#### Track Clicked Push Notification

``` csharp
 _exponea.Track(new Click("action-type", "action-name") { ["campaign_name"] = "My campaign" });
```