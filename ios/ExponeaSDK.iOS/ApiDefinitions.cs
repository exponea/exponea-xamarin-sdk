using System;
using Foundation;
using ObjCRuntime;
using UserNotifications;

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

		// +(BOOL)isExponeaNotificationWithUserInfo:(NSDictionary * _Nonnull)userInfo __attribute__((warn_unused_result("")));
		[Static]
		[Export("isExponeaNotificationWithUserInfo:")]
		bool IsExponeaNotification(NSDictionary userInfo);

		// -(void)handleCampaignClickWithUrl:(NSURL * _Nonnull)url timestamp:(double)timestamp;
		[Export("handleCampaignClickWithUrl:timestamp:")]
		void HandleCampaignClick(NSUrl url, double timestamp);

		// -(void)trackPushTokenWithToken:(NSString * _Nonnull)token;
		[Export("trackPushTokenWithToken:")]
		void TrackPushToken(string token);

		// -(void)setInAppMessageDelegateWithOverrideDefaultBehavior:(BOOL)overrideDefaultBehavior trackActions:(BOOL)trackActions action:(void (^ _Nonnull)(SimpleInAppMessage * _Nonnull, NSString * _Nullable, NSString * _Nullable, BOOL))action;
		[Export("setInAppMessageDelegateWithOverrideDefaultBehavior:trackActions:action:")]
		void SetInAppMessageDelegate(bool overrideDefaultBehavior, bool trackActions, Action<SimpleInAppMessage, NSString, NSString, bool> action);

		// -(void)trackInAppMessageClickWithMessage:(SimpleInAppMessage * _Nonnull)message buttonText:(NSString * _Nullable)buttonText buttonLink:(NSString * _Nullable)buttonLink;
		[Export("trackInAppMessageClickWithMessage:buttonText:buttonLink:")]
		void TrackInAppMessageClick(SimpleInAppMessage message, [NullAllowed] string buttonText, [NullAllowed] string buttonLink);

		// -(void)trackInAppMessageCloseWithMessage:(SimpleInAppMessage * _Nonnull)message;
		[Export("trackInAppMessageCloseWithMessage:")]
		void TrackInAppMessageClose(SimpleInAppMessage message);
	}

	// @interface SimpleInAppMessage : NSObject
	[BaseType(typeof(NSObject), Name = "_TtC15ExponeaSDKProxy18SimpleInAppMessage")]
	[DisableDefaultCtor]
	interface SimpleInAppMessage
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
		[Export("id")]
		string Id { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull rawMessageType;
		[Export("rawMessageType")]
		string RawMessageType { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull rawFrequency;
		[Export("rawFrequency")]
		string RawFrequency { get; }

		// @property (readonly, nonatomic) NSInteger variantId;
		[Export("variantId")]
		int VariantId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull variantName;
		[Export("variantName")]
		string VariantName { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull eventType;
		[Export("eventType")]
		string EventType { get; }

		// @property (readonly, nonatomic) NSInteger priority;
		[Export("priority")]
		int Priority { get; }

		// @property (readonly, nonatomic) NSInteger delayMS;
		[Export("delayMS")]
		int DelayMS { get; }

		// @property (readonly, nonatomic) NSInteger timeoutMS;
		[Export("timeoutMS")]
		int TimeoutMS { get; }

		// -(instancetype _Nonnull)initWithId:(NSString * _Nonnull)id name:(NSString * _Nonnull)name rawMessageType:(NSString * _Nonnull)rawMessageType variantId:(NSInteger)variantId variantName:(NSString * _Nonnull)variantName rawFrequency:(NSString * _Nonnull)rawFrequency eventType:(NSString * _Nonnull)eventType priority:(NSInteger)priority delayMS:(NSInteger)delayMS timeoutMS:(NSInteger)timeoutMS __attribute__((objc_designated_initializer));
		[Export("initWithId:name:rawMessageType:variantId:variantName:rawFrequency:eventType:priority:delayMS:timeoutMS:")]
		[DesignatedInitializer]
		IntPtr Constructor(string id, string name, string rawMessageType, int variantId, string variantName, string rawFrequency, string eventType, int priority, int delayMS, int timeoutMS);

	}
}
