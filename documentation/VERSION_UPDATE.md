# Version update

This guide will help you upgrade your Exponea SDK to the new version.

## Updating from version 0.x.x to 1.x.x
 Changes you will need to do when updating Exponea SDK to version 1 and higher are related to firebase push notifications.

### Changes regarding FirebaseMessagingService

 We decided not to include the implementation of FirebaseMessagingService in our SDK since we want to keep it as small as possible and avoid including the libraries that are not essential for its functionality. SDK no longer has a dependency on the firebase library. If you relied on our FirebaseMessagingService, instead of using yours, changes you need to do are as follows:

1. You will need to implement FirebaseMessagingService on your application side.
2. Call `ExponeaNotificationHandler.Instance.HandleRemoteMessage` when a message is received
3. Call `ExponeaNotificationHandler.Instance.HandleNewToken` when a token is obtained
4. Register this service in your `AndroidManifest.xml` (3 lines before class definition in following example code)

```csharp
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
If you are already using your own FirebaseMessagingService and calling our SDK method in it, just a slight change will be needed.
You will need to change the second parameter when calling the `HandleRemoteMessage` method. Before, SDK accepted the firebase message as the second parameter, but since we removed Firebase dependency, IDictionary<string, string> should be used now. You can get the message data dictionary from Firebase RemoteMessage by calling remoteMessageInstance.Data.