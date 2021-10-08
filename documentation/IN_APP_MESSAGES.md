## In-app messages
Exponea SDK allows you to display native in-app messages based on definitions set up on the Exponea web application. You can find information on how to create your messages in [Exponea documentation](https://docs.exponea.com/docs/in-app-messages).

In-app messages do not require any development work. They work automatically after proper SDK initialization.

### Logging
The SDK logs a lot of useful information about presenting in-app messages on the default `Info` level. To see why each message was/wasn't displayed, make sure your logger level is  `Info` at most. You can set the logger level before initializing the SDK using ` _exponea.LogLevel = LogLevel.Verbose;` on **iOS** and ` _exponea.LogLevel = LogLevel.Debug;` on **Android**

### Displaying in-app messages
In-app messages are triggered when an event is tracked based on conditions setup on the Exponea backend. Once a message passes those filters, the SDK will try to present the message. 

#### On Android

The SDK hooks into the application lifecycle, and every time an activity is resumed, it will remember it and use it for presenting an in-app message. Messages are displayed in a new Activity that is started for them (except for the slide-in message that is directly injected into the currently running Activity).

#### On iOS

Once a message passes the filters, the SDK will try to present the message in the top-most `presentedViewController` (except for the slide-in message that uses `UIWindow` directly).
