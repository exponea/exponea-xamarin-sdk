using System;
using Foundation;
using ObjCRuntime;
using UserNotifications;
using UIKit;

namespace ExponeaSdk
{
	// @interface Exponea : NSObject
	[BaseType (typeof(NSObject))]
	interface Exponea
	{
		// @property (readonly, nonatomic, strong, class) Exponea * _Nonnull instance;
		[Static]
		[Export ("instance", ArgumentSemantic.Strong)]
		Exponea Instance { get; }

		// -(void)configureWithConfiguration:(NSDictionary * _Nonnull)configuration;
		[Export ("configureWithConfiguration:")]
		void Configure(NSDictionary configuration);

		// -(void)trackEventWithEventType:(NSString * _Nonnull)eventType properties:(NSDictionary * _Nonnull)properties timestamp:(double)timestamp;
		[Export ("trackEventWithEventType:properties:timestamp:")]
		void TrackEvent(string eventType, NSDictionary properties, double timestamp);

		// -(void)anonymizeWithExponeaProjectDictionary:(NSDictionary * _Nullable)exponeaProjectDictionary projectMappingDictionary:(NSDictionary * _Nullable)projectMappingDictionary;
		[Export ("anonymizeWithExponeaProjectDictionary:projectMappingDictionary:")]
		void Anonymize([NullAllowed] NSDictionary exponeaProjectDictionary, [NullAllowed] NSDictionary projectMappingDictionary);

		// -(void)anonymize;
		[Export ("anonymize")]
		void Anonymize ();

		// -(void)anonymizeWithExponeaProjectDictionary:(NSDictionary * _Nullable)exponeaProjectDictionary;
		[Export ("anonymizeWithExponeaProjectDictionary:")]
		void Anonymize([NullAllowed] NSDictionary exponeaProjectDictionary);

		// -(void)flushData;
		[Export ("flushData")]
		void FlushData ();

		// -(void)flushDataWithDone:(void (^ _Nonnull)(NSString * _Nonnull))done;
		[Export ("flushDataWithDone:")]
		void FlushData (Action<NSString> done);

		// -(NSString * _Nullable)getCustomerCookie __attribute__((warn_unused_result("")));
		[NullAllowed, Export ("getCustomerCookie")]
		string CustomerCookie { get; }

		// -(BOOL)isConfigured __attribute__((warn_unused_result("")));
		[Export ("isConfigured")]
		bool IsConfigured { get; }

		// -(void)checkPushSetup;
		[Export ("checkPushSetup")]
		void CheckPushSetup ();

		// -(BOOL)isSaveModeEnabled __attribute__((warn_unused_result("")));
		[Export ("isSaveModeEnabled")]
		bool IsSaveModeEnabled { get; }

		// -(BOOL)isAutoSessionTrackingEnabled __attribute__((warn_unused_result("")));
		[Export ("isAutoSessionTrackingEnabled")]
		bool IsAutoSessionTrackingEnabled { get; }

		// -(NSString * _Nonnull)getFlushMode __attribute__((warn_unused_result("")));
		[Export ("getFlushMode")]
		string FlushMode { get; }

		// -(void)setFlushModeWithFlushMode:(NSString * _Nonnull)flushMode;
		[Export ("setFlushModeWithFlushMode:")]
		void SetFlushMode (string flushMode);

		// -(NSInteger)getFlushPeriod __attribute__((warn_unused_result("")));
		[Export ("getFlushPeriod")]
		nint FlushPeriod { get; }

		// -(void)setFlushPeriodWithFlushPeriod:(NSNumber * _Nonnull)flushPeriod;
		[Export ("setFlushPeriodWithFlushPeriod:")]
		void SetFlushPeriod (NSNumber flushPeriod);

		// -(NSString * _Nonnull)getLogLevel __attribute__((warn_unused_result("")));
		[Export ("getLogLevel")]
		string LogLevel { get; }

		// -(void)setLogLevelWithLogLevel:(NSString * _Nonnull)logLevel;
		[Export ("setLogLevelWithLogLevel:")]
		void SetLogLevel (string logLevel);

		// -(NSString * _Nonnull)getDefaultProperties __attribute__((warn_unused_result("")));
		[Export ("getDefaultProperties")]
		string DefaultProperties { get; }

		// -(void)setDefaultPropertiesWithProperties:(NSDictionary * _Nonnull)properties;
		[Export ("setDefaultPropertiesWithProperties:")]
		void SetDefaultProperties (NSDictionary properties);

		// -(void)identifyCustomerWithCustomerIds:(NSDictionary * _Nonnull)customerIds properties:(NSDictionary * _Nonnull)properties;
		[Export ("identifyCustomerWithCustomerIds:properties:")]
		void IdentifyCustomer (NSDictionary customerIds, NSDictionary properties);

		// -(void)trackSessionStart;
		[Export ("trackSessionStart")]
		void TrackSessionStart ();

		// -(void)trackSessionEnd;
		[Export ("trackSessionEnd")]
		void TrackSessionEnd ();

		// -(void)trackPushOpenedWithUserInfo:(NSDictionary * _Nonnull)userInfo;
		[Export ("trackPushOpenedWithUserInfo:")]
		void TrackPushOpened(NSDictionary userInfo);

		// -(void)trackPaymentWithProperties:(NSDictionary * _Nonnull)properties timestamp:(double)timestamp;
		[Export ("trackPaymentWithProperties:timestamp:")]
		void TrackPayment (NSDictionary properties, double timestamp);

		// -(void)fetchConsentsWithSuccess:(void (^ _Nonnull)(NSString * _Nonnull))success fail:(void (^ _Nonnull)(NSString * _Nonnull))fail;
		[Export ("fetchConsentsWithSuccess:fail:")]
		void FetchConsents (Action<NSString> success, Action<NSString> fail);

		// -(void)fetchRecommendationsWithOptionsDictionary:(NSDictionary * _Nonnull)optionsDictionary success:(void (^ _Nonnull)(NSString * _Nonnull))success fail:(void (^ _Nonnull)(NSString * _Nonnull))fail;
		[Export ("fetchRecommendationsWithOptionsDictionary:success:fail:")]
		void FetchRecommendations (NSDictionary optionsDictionary, Action<NSString> success, Action<NSString> fail);

		// -(void)handlePushNotificationTokenWithDeviceToken:(NSData * _Nonnull)deviceToken;
		[Export("handlePushNotificationTokenWithDeviceToken:")]
		void HandlePushNotificationToken(NSData deviceToken);

		// -(void)handlePushNotificationOpenedWithResponse:(UNNotificationResponse * _Nonnull)response;
		[Export("handlePushNotificationOpenedWithResponse:")]
		void HandlePushNotificationOpened(UNNotificationResponse response);

		// -(void)handlePushNotificationOpenedWithUserInfo:(NSDictionary * _Nonnull)userInfo actionIdentifier:(NSString * _Nullable)actionIdentifier;
		[Export("handlePushNotificationOpenedWithUserInfo:actionIdentifier:")]
		void HandlePushNotificationOpened(NSDictionary userInfo, [NullAllowed] string actionIdentifier);

		// -(void)processNotificationRequestWithRequest:(UNNotificationRequest * _Nonnull)request contentHandler:(void (^ _Nonnull)(UNNotificationContent * _Nonnull))contentHandler;
		[Export("processNotificationRequestWithRequest:contentHandler:")]
		void ProcessNotificationRequest(UNNotificationRequest request, Action<UNNotificationContent> contentHandler);

		// -(void)serviceExtensionTimeWillExpire;
		[Export("serviceExtensionTimeWillExpire")]
		void ServiceExtensionTimeWillExpire();

		// -(void)notificationReceived:(UNNotification * _Nonnull)notification context:(NSExtensionContext * _Nullable)context viewController:(UIViewController * _Nonnull)viewController;
		[Export("notificationReceived:context:viewController:")]
		void NotificationReceived(UNNotification notification, [NullAllowed] NSExtensionContext context, UIViewController viewController);

		// +(BOOL)isExponeaNotificationWithUserInfo:(NSDictionary * _Nonnull)userInfo __attribute__((warn_unused_result("")));
		[Static]
		[Export("isExponeaNotificationWithUserInfo:")]
		bool IsExponeaNotification(NSDictionary userInfo);

		// -(void)initNotificationServiceWithAppGroup:(NSString * _Nonnull)appGroup __attribute__((objc_method_family("none")));
		[Export("initNotificationServiceWithAppGroup:")]
		void InitNotificationService(string appGroup);

		// -(void)handleCampaignClickWithUrl:(NSURL * _Nonnull)url timestamp:(double)timestamp;
		[Export("handleCampaignClickWithUrl:timestamp:")]
		void HandleCampaignClick(NSUrl url, double timestamp);

		// -(void)trackPushTokenWithToken:(NSString * _Nonnull)token;
		[Export("trackPushTokenWithToken:")]
		void TrackPushToken(string token);
	}
}
