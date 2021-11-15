using System;
using ExponeaSDKNotificationsProxy;
using Foundation;
using UIKit;
using UserNotifications;

namespace ExponeaSdkNotifications
{
	// @interface ExponeaNotificationHandler : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface ExponeaNotificationHandler
	{
		// -(instancetype _Nonnull)initWithAppGroup:(NSString * _Nonnull)appGroup __attribute__((objc_designated_initializer));
		[Export ("initWithAppGroup:")]
		[DesignatedInitializer]
		IntPtr Constructor (string appGroup);

		// -(void)processNotificationRequestWithRequest:(UNNotificationRequest * _Nonnull)request contentHandler:(void (^ _Nonnull)(UNNotificationContent * _Nonnull))contentHandler;
		[Export ("processNotificationRequestWithRequest:contentHandler:")]
		void ProcessNotificationRequestWithRequest (UNNotificationRequest request, Action<UNNotificationContent> contentHandler);

		// -(void)serviceExtensionTimeWillExpire;
		[Export ("serviceExtensionTimeWillExpire")]
		void ServiceExtensionTimeWillExpire ();

		// -(void)notificationReceived:(UNNotification * _Nonnull)notification context:(NSExtensionContext * _Nullable)context viewController:(UIViewController * _Nonnull)viewController;
		[Export ("notificationReceived:context:viewController:")]
		void NotificationReceived (UNNotification notification, [NullAllowed] NSExtensionContext context, UIViewController viewController);
	}
}
