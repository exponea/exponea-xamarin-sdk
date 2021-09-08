

## 🔍 Configuration

Before using most of the SDK functionality, you'll need to configure Exponea to connect it to backend application. Configuration consists of these properties:

#### ProjectToken

* Is your project token which can be found in the Exponea APP ```Project``` -> ```Overview```
* If you need to switch project settings during runtime of the application, you can use [Anonymize feature](./ANONYMIZE.md)

#### Authorization

* Exponea **public** key.
* For more information, please see [Exponea API documentation](https://docs.exponea.com/reference#access-keys)

#### BaseURL

* Base url of your Exponea deployment.
* Default value `https://api.exponea.com`

#### ProjectRouteMap

* In case you have more than one project to track for one event, you should provide which "Routes" (tracking events) each project token should be track.

For detailed information, please go to [Project Mapping documentation](../Documentation/PROJECT_MAPPING.md)

#### MaxTries

* Maximum number of retries to flush data to Exponea API.
* SDK will consider the value to be flushed if this number is exceed and delete from the queue.

#### SessionTimeout

When the application is closed, the SDK doesn't track end of session right away, but waits a bit for the user to come back before doing so. You can configure the timeout by setting this property.

#### AutomaticSessionTracking

* Flag to control the automatic tracking of user sessions.
* When set to true, the SDK will
automatically send `session_start` and `session_end` events to Exponea API
* You can opt-out by setting this flag to false and implement your own session tracking.

#### DefaultProperties

* The properties defined on this setting will always be sent with all triggered tracking events. 

#### TokenTrackFrequency

*You can define your policy for tracking push notification token. Default value `OnTokenChange` is recommended.

#### AndroidConfiguration
 Specific configuration for Android

#### iOSConfiguration
Specific configuration for iOS


### Android specific properties 

#### AutomaticPushNotification

* Controls if the SDK will handle push notifications automatically.

#### PushIcon

* Icon to be displayed when show a push notification.

#### PushAccentColor

* Accent color of push notification. Changes color of small icon and notification buttons. e.g. `Color.GREEN`
    > This is a color id, not a resource id. When using colors from resources you have to get the resource first: `context.resources.getColor(R.color.something)`

#### PushChannelName

* Name of the Channel to be created for the push notifications.
* Only available for API level 26+. More info [here](https://developer.android.com/training/notify-user/channels)

#### PushChannelDescription

* Description of the Channel to be created for the push notifications.
* Only available for API level 26+. More info [here](https://developer.android.com/training/notify-user/channels)

#### PushChannelId

* Channel ID for push notifications.
* Only available for API level 26+. More info [here](https://developer.android.com/training/notify-user/channels)

#### PushNotificationImportance

* Notification importance for the notification channel.
* Only available for API level 26+. More info [here](https://developer.android.com/training/notify-user/channels)

### iOS specific properties and configuration example

#### RequirePushAuthorization 
If true, push notification registration and push token tracking is only done if the device is authorized to display push notifications. Unless you're using silent notifications, keep the default value `true`.

#### AppGroup
 App group used for communication between main app and notification extensions. This is a required field for Rich push notification setup


#### Example
``` csharp
var config = new Configuration("project-token", "your-auth-token", "https://api.exponea.com");
config.AutomaticSessionTracking = false;
config.DefaultProperties = new Dictionary<string, object>()
            {
                { "thisIsADefaultStringProperty", "This is a default string value" },
                { "thisIsADefaultIntProperty", 1},
                { "thisIsADefaultDoubleProperty", 12.53623}

            };
 config.AndroidConfiguration = new AndroidConfiguration(
                automaticPushNotification: false
            );
_exponea.Configure(config);
```