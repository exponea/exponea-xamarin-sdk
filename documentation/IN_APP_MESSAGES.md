## In-app messages
Exponea SDK allows you to display native in-app messages based on definitions set up on the Exponea web application. You can find information on how to create your messages in [Exponea documentation](https://docs.exponea.com/docs/in-app-messages).

In-app messages do not require any development work. They work automatically after proper SDK initialization.

### Logging
The SDK logs a lot of useful information about presenting in-app messages on the default `Info` level. To see why each message was/wasn't displayed, make sure your logger level is  `Info` at most. You can set the logger level before initializing the SDK using ` _exponea.LogLevel = LogLevel.Verbose;` on **iOS** and ` _exponea.LogLevel = LogLevel.Debug;` on **Android**

### Displaying in-app messages
In-app messages are triggered when an event is tracked based on conditions setup on the Exponea backend. Once a message passes those filters, the SDK will try to present the message.

Message is able to be shown only if it is loaded and also its image is loaded too. In case that message is not yet fully loaded (including its image) then the request-to-show is registered in SDK for that message so SDK will show it after full load.
Due to prevention of unpredicted behaviour (i.e. image loading takes too long) that request-to-show has timeout of 3 seconds.

> If message loading hits timeout of 3 seconds then message will be shown on 'next request'. For example the 'session_start' event triggers a showing of message that needs to be fully loaded but it timeouts, then message will not be shown. But it will be ready for next `session_start` event so it will be shown on next 'application run'.

#### On Android

The SDK hooks into the application lifecycle, and every time an activity is resumed, it will remember it and use it for presenting an in-app message. Messages are displayed in a new Activity that is started for them (except for the slide-in message that is directly injected into the currently running Activity).

#### On iOS

Once a message passes the filters, the SDK will try to present the message in the top-most `presentedViewController` (except for the slide-in message that uses `UIWindow` directly).

### In-app messages loading
In-app messages reloading is triggered by any case of:
- when `Exponea.identifyCustomer` is called
- when `Exponea.anonymize` is called
- when any event is tracked (except Push clicked, opened or session ends) and In-app messages cache is older then 30 minutes from last load
  Any In-app message images are preloaded too so message is able to be shown after whole process is finished. Please considers it while testing of In-app feature.
  It is common behaviour that if you change an In-app message data on platform then this change is reflected in SDK after 30 minutes due to usage of messages cache. Do call `Exponea.identifyCustomer` or `Exponea.anonymize` if you want to reflect changes immediately.

### Custom in-app message actions
If you want to override default SDK behavior, when in-app message action is performed (button is clicked, a message is closed), or you want to add your code to be performed along with code executed by the SDK, you can set up InAppMessageDelegate by calling `SetInAppMessageDelegate` on Exponea instance.

```csharp
 _exponea.SetInAppMessageDelegate(
 	overrideDefaultBehavior: false, //If overrideDefaultBehavior is set to true, default in-app action will not be performed ( e.g. deep link )
 	trackActions: true, // If trackActions is set to false, click and close in-app events will not be tracked automatically
 	action: delegate (InAppMessage message, string buttonText, string buttonUrl, bool interaction) {
    // Here goes the code you want to be executed on in-app message action
    // On in-app click, the buttonText and buttonUrl contain button info, and the interaction is true
    // On in-app close, the buttonText and buttonUrl are null, and the interaction is false.
 });
```
If you set `trackActions` to **false** but you still want to track click/close event under some circumstances, you can call Exponea methods `TrackInAppMessageClick` or `TrackInAppMessageClose`.

```csharp
 _exponea.SetInAppMessageDelegate(
 	overrideDefaultBehavior: true, 
 	trackActions: false, 
 	action: delegate (InAppMessage message, string buttonText, string buttonUrl, bool interaction) {
 	if (<your-special-condition>) {
	    if (interaction) {
	        _exponea.TrackInAppMessageClick(message, buttonText, buttonUrl);
	    } else {
	        _exponea.TrackInAppMessageClose(message);
	    }
    }
 });
```

Method `TrackInAppMessageClose` will track a 'close' event with 'interaction' field of TRUE value by default. You are able to use a optional parameter 'interaction' of this method to override this value.

> The behaviour of `TrackInAppMessageClick` and `TrackInAppMessageClose` may be affected by the tracking consent feature, which in enabled mode considers the requirement of explicit consent for tracking. Read more in [tracking consent documentation](./TRACKING_CONSENT.md).
