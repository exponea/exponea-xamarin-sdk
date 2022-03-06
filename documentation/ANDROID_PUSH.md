## 📣  Android Push Notifications

Exponea allows you to easily create complex scenarios which you can use to send push notifications directly to your customers. The following section explains how to enable push notifications.

## Push Integrations
Exponea SDK supports these integrations:

- [Standard (Firebase) integration](#firebase-integration)
- [Huawei integration](#huawei-integration)

## Automatic tracking of Push Notifications

In the [Exponea SDK configuration](CONFIG.md), you can enable or disable the automatic push notification tracking by setting the Boolean value to the `AutomaticPushNotification` property and potentially setting up the desired frequency to the `TokenTrackFrequency`(default value is ON_TOKEN_CHANGE).

With `AutomaticPushNotification` enabled, the SDK will correctly display push notifications from Exponea and track a "campaign" event for every delivered/opened push notification with the correct properties.


## Firebase integration 

For push notifications to work, you'll need to set up a few things:
- create a Firebase project and integrate Firebase into your application. [Official Firebase documentation](https://firebase.google.com/docs/android/setup#console) describes this process.
- set the Firebase server key in the Exponea web app
- add a broadcast listener for opening push notifications

### Creating FirebaseMessagingService

To handle incoming push messages, you will have to create your FirebaseMessagingService implementation, which calls the SDK method `HandleRemoteMessage` when the message is received and `HandleNewToken` when the token is obtained from the Firebase.

``` csharp
...
using Exponea.Android;
using Firebase.Messaging;

namespace YourNameSpace
{
    [Service(Name = "yournamespace.ExampleFirebaseMessageService", Exported = false)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class ExampleFirebaseMessageService : FirebaseMessagingService {

            public override void OnMessageReceived(RemoteMessage message)
            {
                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                if (!ExponeaNotificationHandler.Instance.HandleRemoteMessage(ApplicationContext, message.Data, notificationManager, true))
                {
                    // push notification is from another push provider
                }
            }

            public override void OnNewToken(string token)
            {
                ExponeaNotificationHandler.Instance.HandleNewToken(ApplicationContext, token);
            }
    }
}
```

## Huawei integration
Newer phones manufactured by [Huawei](https://huaweimobileservices.com/)  come with Huawei Mobile Services (HMS). It's a service used to deliver push _instead of_ Google's Firebase Cloud Messaging (FCM).

### Integrating HMS into your project
To send/receive push notifications, you have to:
1. register and set up a [Huawei Developer account](https://developer.huawei.com/consumer/en/console)
2. create a project and App in AppGallery Connect
3. generate and configure a Signing Certificate
4. enable push kit in AppGallery Connect APIs
5. Add needed dependencies and `agconnect-services.json` to your app
6. configure the Signing Information in your app

Steps are described in detail in the official [Huawei documentation](https://developer.huawei.com/consumer/en/doc/development/AppGallery-connect-Guides/agc-get-started-xamarin-0000001081055220)

**Do not** forget to configure the Signing Certificate Fingerprint in AppGallery Connect and add signing information to your app. Without it, push notifications will not work. 

We also recommend adding this line to your `AssemblyInfo.cs`. 
```
[assembly: MetaData("push_kit_auto_init_enabled", Value = "true")]
```
Some of the necessary steps may not be mentioned directly in Huawei documentation for Xamarin but are mentioned in  [Huawei documentation for Android](https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#0), and we recommend checking it if something is not working for you.

### Creating HmsMessageService
To handle incoming push messages, you will have to create your HmsMessageService implementation, which calls the SDK method `HandleRemoteMessage` when the message is received and `HandleNewHmsToken` when the token is obtained from the Huawei Messaging Service.

``` csharp
...
using Exponea.Android;
using Huawei.Hms.Push;

namespace YourNameSpace
{
[Service(Name = "yournamespace.ExampleHuaweiMessageService", Exported = false)]
    [IntentFilter(new[] { "com.huawei.push.action.MESSAGING_EVENT" })]
    public class ExampleHuaweiMessageService : HmsMessageService {

            public override void OnMessageReceived(RemoteMessage message)
            { 
                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                if (!ExponeaNotificationHandler.Instance.HandleRemoteMessage(ApplicationContext, message.DataOfMap, notificationManager, true))
                {
                    // push notification is from another push provider
                }
            }

            public override void OnNewToken(string token)
            {
                ExponeaNotificationHandler.Instance.HandleNewHmsToken(ApplicationContext, token);
            }
    }
}
```

Exponea SDK will only handle push notification messages coming from Exponea servers. You can also use the helper method `IsExponeaNotification()`.

## Responding to Push notifications

When creating a notification using Exponea Web App, you can choose from 3 different actions to be used when tapping the message or additional buttons on a notification.

### 1. Open app
Open app action generates an intent with action `com.exponea.sdk.action.PUSH_CLICKED`. To respond to it, you need to set up a BroadcastReceiver.

``` csharp
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

            // Process the data if you need to
            foreach (KeyValuePair<string, Java.Lang.Object> entry in value.Attributes)
            {
                Console.WriteLine(entry.Key + ":" + entry.Value);
            }
            
            // Do not start an Activity here. It is forbidden when targetting Android 12. The SDK will start the activity on push open for you
        }
    }
}
```

In the BroadcastReceiver, you can launch a corresponding activity(e.g., your main activity). Campaign data is included in the intent as `ExponeaPushReceiver.ExtraData`.


### 2. Deep-link
Deep-link action creates "view" intent that contains the URL specified when setting up this action. To respond to this intent, create an intent filter on the activity that should handle it. 
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
The open web browser action is handled automatically by the SDK, and no work is required from the developer to handle it.

## Handling notification payload extra data
  You can set up notifications to contain an extra data payload. Whenever a message arrives, the Exponea SDK will call `NotificationDataCallback` that you can set on the `ExponeaNotificationHandler` instance. The extras are a `Dictionary<string, object>`.

#### 💻 Usage

``` csharp
Action<Dictionary<string, object>> action = (extra) =>
            {
                //handle extra values
            };
ExponeaNotificationHandler.Instance.SetNotificationDataCallback(action);
```

Note that if previous data was received and no listener was attached to the callback, that data will be dispatched as soon as a listener is attached.

> If your app is not running in the background, the SDK will auto-initialize when a push notification is received. In this case, `NotificationDataCallback` is not set, so the callback will be called after attaching the listener(next app start). If you need to respond to the notification received immediately, implement your own `FirebaseMessagingService` and set the notification data callback in the `OnMessageReceived` function before calling `HandleRemoteMessage` on the ExponeaNotificationHandler instance. 


## Silent push notifications
Exponea web app allows you to set up silent push notifications that are not displayed to the user. The SDK tracks the` campaign` event when the push notification is delivered, just like for regular notifications. There is no opening for those notifications, but if you have set up extra data in the payload, the SDK will call `NotificationDataCallback` as described in [Handling notification payload extra data](#handling-notification-payload-extra-data).

## Manual tracking of Push Notifications
If you decide to deactivate the automatic push notification or wish to track push notifications from other providers, you can still track events manually.

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