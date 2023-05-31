using System;
using AVFoundation;
using Foundation;
using ObjCRuntime;
using UIKit;
using UserNotifications;

namespace ExponeaSdk
{
	// @interface AppInboxAction : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC15ExponeaSDKProxy14AppInboxAction")]
	[DisableDefaultCtor]
	interface AppInboxAction
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable action;
		[NullAllowed, Export ("action")]
		string Action { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable title;
		[NullAllowed, Export ("title")]
		string Title { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable url;
		[NullAllowed, Export ("url")]
		string Url { get; }

		// -(instancetype _Nonnull)initWithAction:(NSString * _Nullable)action title:(NSString * _Nullable)title url:(NSString * _Nullable)url __attribute__((objc_designated_initializer));
		[Export ("initWithAction:title:url:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] string action, [NullAllowed] string title, [NullAllowed] string url);
	}

	// @interface AppInboxMessage : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC15ExponeaSDKProxy15AppInboxMessage")]
	[DisableDefaultCtor]
	interface AppInboxMessage : INativeObject
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
		[Export ("id")]
		string Id { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (nonatomic) BOOL read;
		[Export ("read")]
		bool Read { get; set; }

		// @property (readonly, nonatomic) NSInteger receivedTime;
		[Export ("receivedTime")]
		int ReceivedTime { get; }

		// @property (readonly, nonatomic, strong) NSDictionary * _Nonnull content;
		[Export ("content", ArgumentSemantic.Strong)]
		NSDictionary Content { get; }

		// -(instancetype _Nonnull)initWithId:(NSString * _Nonnull)id type:(NSString * _Nonnull)type read:(BOOL)read receivedTime:(NSInteger)receivedTime content:(NSDictionary * _Nonnull)content __attribute__((objc_designated_initializer));
		[Export ("initWithId:type:read:receivedTime:content:")]
		[DesignatedInitializer]
		IntPtr Constructor (string id, string type, bool read, int receivedTime, NSDictionary content);
	}

	// @protocol AuthorizationProviderType
	[Protocol]
	interface AuthorizationProviderType
	{
		// @required -(NSString * _Nullable)getAuthorizationToken __attribute__((warn_unused_result("")));
		[Abstract]
		[NullAllowed, Export ("getAuthorizationToken")]
		string AuthorizationToken { get; }
	}

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

		// -(void)trackEventWithEventType:(NSString * _Nonnull)eventType properties:(NSDictionary * _Nonnull)properties;
		[Export ("trackEventWithEventType:properties:")]
		void TrackEvent (string eventType, NSDictionary properties);

		// -(void)anonymizeWithExponeaProjectDictionary:(NSDictionary * _Nullable)exponeaProjectDictionary projectMappingDictionary:(NSDictionary * _Nullable)projectMappingDictionary;
		[Export ("anonymizeWithExponeaProjectDictionary:projectMappingDictionary:")]
		void Anonymize([NullAllowed] NSDictionary exponeaProjectDictionary, [NullAllowed] NSDictionary projectMappingDictionary);

		// -(void)anonymize;
		[Export ("anonymize")]
		void Anonymize ();

		// -(void)anonymizeWithExponeaProjectDictionary:(NSDictionary * _Nullable)exponeaProjectDictionary;
		[Export ("anonymizeWithExponeaProjectDictionary:")]
		void Anonymize([NullAllowed] NSDictionary exponeaProjectDictionary);

		// -(void)handlePushNotificationTokenWithDeviceToken:(NSData * _Nonnull)deviceToken;
		[Export ("handlePushNotificationTokenWithDeviceToken:")]
		void HandlePushNotificationToken (NSData deviceToken);

		// -(void)handlePushNotificationOpenedWithResponse:(UNNotificationResponse * _Nonnull)response;
		[Export ("handlePushNotificationOpenedWithResponse:")]
		void HandlePushNotificationOpened (UNNotificationResponse response);

		// -(void)handlePushNotificationOpenedWithUserInfo:(NSDictionary * _Nonnull)userInfo actionIdentifier:(NSString * _Nullable)actionIdentifier;
		[Export ("handlePushNotificationOpenedWithUserInfo:actionIdentifier:")]
		void HandlePushNotificationOpened (NSDictionary userInfo, [NullAllowed] string actionIdentifier);

		// -(void)handlePushNotificationOpenedWithoutTrackingConsentWithUserInfo:(NSDictionary * _Nonnull)userInfo actionIdentifier:(NSString * _Nullable)actionIdentifier;
		[Export ("handlePushNotificationOpenedWithoutTrackingConsentWithUserInfo:actionIdentifier:")]
		void HandlePushNotificationOpenedWithoutTrackingConsentWithUserInfo (NSDictionary userInfo, [NullAllowed] string actionIdentifier);

		// -(void)handleCampaignClickWithUrl:(NSURL * _Nonnull)url timestamp:(double)timestamp;
		[Export ("handleCampaignClickWithUrl:timestamp:")]
		void HandleCampaignClick (NSUrl url, double timestamp);

		// -(void)trackCampaignClickWithUrl:(NSURL * _Nonnull)url timestamp:(double)timestamp;
		[Export ("trackCampaignClickWithUrl:timestamp:")]
		void TrackCampaignClickWithUrl (NSUrl url, double timestamp);

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

		// -(void)trackPushOpenedWithoutTrackingConsentWithUserInfo:(NSDictionary * _Nonnull)userInfo;
		[Export ("trackPushOpenedWithoutTrackingConsentWithUserInfo:")]
		void TrackPushOpenedWithoutTrackingConsent (NSDictionary userInfo);

		// -(void)trackPushReceivedWithUserInfo:(NSDictionary * _Nonnull)userInfo;
		[Export ("trackPushReceivedWithUserInfo:")]
		void TrackPushReceived (NSDictionary userInfo);

		// -(void)trackPushReceivedWithoutTrackingConsentWithUserInfo:(NSDictionary * _Nonnull)userInfo;
		[Export ("trackPushReceivedWithoutTrackingConsentWithUserInfo:")]
		void TrackPushReceivedWithoutTrackingConsent (NSDictionary userInfo);

		// -(void)trackPushTokenWithToken:(NSString * _Nonnull)token;
		[Export ("trackPushTokenWithToken:")]
		void TrackPushToken (string token);

		// -(void)trackPaymentWithProperties:(NSDictionary * _Nonnull)properties timestamp:(double)timestamp;
		[Export ("trackPaymentWithProperties:timestamp:")]
		void TrackPayment (NSDictionary properties, double timestamp);

		// -(void)fetchConsentsWithSuccess:(void (^ _Nonnull)(NSString * _Nonnull))success fail:(void (^ _Nonnull)(NSString * _Nonnull))fail;
		[Export ("fetchConsentsWithSuccess:fail:")]
		void FetchConsents (Action<NSString> success, Action<NSString> fail);

		// -(void)fetchRecommendationsWithOptionsDictionary:(NSDictionary * _Nonnull)optionsDictionary success:(void (^ _Nonnull)(NSString * _Nonnull))success fail:(void (^ _Nonnull)(NSString * _Nonnull))fail;
		[Export ("fetchRecommendationsWithOptionsDictionary:success:fail:")]
		void FetchRecommendations (NSDictionary optionsDictionary, Action<NSString> success, Action<NSString> fail);

		// +(BOOL)isExponeaNotificationWithUserInfo:(NSDictionary * _Nonnull)userInfo __attribute__((warn_unused_result("")));
		[Static]
		[Export("isExponeaNotificationWithUserInfo:")]
		bool IsExponeaNotification(NSDictionary userInfo);

		// -(void)setInAppMessageDelegateWithOverrideDefaultBehavior:(BOOL)overrideDefaultBehavior trackActions:(BOOL)trackActions action:(void (^ _Nonnull)(SimpleInAppMessage * _Nonnull, NSString * _Nullable, NSString * _Nullable, BOOL))action;
		[Export("setInAppMessageDelegateWithOverrideDefaultBehavior:trackActions:action:")]
		void SetInAppMessageDelegate(bool overrideDefaultBehavior, bool trackActions, Action<SimpleInAppMessage, NSString, NSString, bool> action);

		// -(void)trackInAppMessageClickWithMessage:(SimpleInAppMessage * _Nonnull)message buttonText:(NSString * _Nullable)buttonText buttonLink:(NSString * _Nullable)buttonLink;
		[Export("trackInAppMessageClickWithMessage:buttonText:buttonLink:")]
		void TrackInAppMessageClick(SimpleInAppMessage message, [NullAllowed] string buttonText, [NullAllowed] string buttonLink);

		// -(void)trackInAppMessageClickWithoutTrackingConsentWithMessage:(SimpleInAppMessage * _Nonnull)message buttonText:(NSString * _Nullable)buttonText buttonLink:(NSString * _Nullable)buttonLink;
		[Export ("trackInAppMessageClickWithoutTrackingConsentWithMessage:buttonText:buttonLink:")]
		void TrackInAppMessageClickWithoutTrackingConsent (SimpleInAppMessage message, [NullAllowed] string buttonText, [NullAllowed] string buttonLink);

		// -(void)trackInAppMessageCloseWithMessage:(SimpleInAppMessage * _Nonnull)message isUserInteraction:(BOOL)isUserInteraction;
		[Export ("trackInAppMessageCloseWithMessage:isUserInteraction:")]
		void TrackInAppMessageClose (SimpleInAppMessage message, bool isUserInteraction);

		// -(void)trackInAppMessageCloseWithoutTrackingConsentWithMessage:(SimpleInAppMessage * _Nonnull)message isUserInteraction:(BOOL)isUserInteraction;
		[Export ("trackInAppMessageCloseWithoutTrackingConsentWithMessage:isUserInteraction:")]
		void TrackInAppMessageCloseWithoutTrackingConsent (SimpleInAppMessage message, bool isUserInteraction);

		// -(void)setAppInboxProvider:(NSDictionary * _Nonnull)data;
		[Export ("setAppInboxProviderWithData:")]
		void SetAppInboxProviderWithData (NSDictionary data);

		// -(void)getAppInboxButton;
		[Export("getAppInboxButton")]
		NSObject getAppInboxButton();

		// -(void)trackAppInboxOpened:(AppInboxMessage * _Nonnull)message;
		[Export ("trackAppInboxOpened:")]
		void TrackAppInboxOpened (AppInboxMessage message);

		// -(void)trackAppInboxOpenedWithoutTrackingConsent:(AppInboxMessage * _Nonnull)message;
		[Export ("trackAppInboxOpenedWithoutTrackingConsent:")]
		void TrackAppInboxOpenedWithoutTrackingConsent (AppInboxMessage message);

		// -(void)trackAppInboxClick:(AppInboxAction * _Nonnull)action :(AppInboxMessage * _Nonnull)message;
		[Export ("trackAppInboxClick::")]
		void TrackAppInboxClick (AppInboxAction action, AppInboxMessage message);

		// -(void)trackAppInboxClickWithoutTrackingConsent:(AppInboxAction * _Nonnull)action :(AppInboxMessage * _Nonnull)message;
		[Export ("trackAppInboxClickWithoutTrackingConsent::")]
		void TrackAppInboxClickWithoutTrackingConsent (AppInboxAction action, AppInboxMessage message);

		// -(void)fetchAppInboxWithCompletion:(void (^ _Nonnull)(NSArray<AppInboxMessage *> * _Nonnull))completion errorCompletion:(void (^ _Nonnull)(NSString * _Nonnull))errorCompletion;
		[Export ("fetchAppInboxWithCompletion:errorCompletion:")]
		void FetchAppInbox (Action<NSArray<AppInboxMessage>> completion, Action<NSString> errorCompletion);

		// -(void)fetchAppInboxItemWithMessageId:(NSString * _Nonnull)messageId completion:(void (^ _Nonnull)(AppInboxMessage * _Nonnull))completion errorCompletion:(void (^ _Nonnull)(NSString * _Nonnull))errorCompletion;
		[Export ("fetchAppInboxItemWithMessageId:completion:errorCompletion:")]
		void FetchAppInboxItemWithMessageId (string messageId, Action<AppInboxMessage> completion, Action<NSString> errorCompletion);

		// -(void)markAppInboxAsRead:(AppInboxMessage * _Nonnull)message completion:(void (^ _Nonnull)(BOOL))completion errorCompletion:(void (^ _Nonnull)(NSString * _Nonnull))errorCompletion;
		[Export ("markAppInboxAsRead:completion:errorCompletion:")]
		void MarkAppInboxAsRead (AppInboxMessage message, Action<bool> completion, Action<NSString> errorCompletion);
	}

	// @interface ExponeaXamarinVersion : NSObject
	[BaseType (typeof(NSObject))]
	interface ExponeaXamarinVersion
	{
	}

	// @interface SimpleInAppMessage : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC15ExponeaSDKProxy18SimpleInAppMessage")]
	[DisableDefaultCtor]
	interface SimpleInAppMessage
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
		[Export ("id")]
		string Id { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull rawMessageType;
		[Export ("rawMessageType")]
		string RawMessageType { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull rawFrequency;
		[Export ("rawFrequency")]
		string RawFrequency { get; }

		// @property (readonly, nonatomic) NSInteger variantId;
		[Export("variantId")]
		int VariantId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull variantName;
		[Export ("variantName")]
		string VariantName { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull eventType;
		[Export ("eventType")]
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

		// @property (readonly, copy, nonatomic) NSString * _Nullable payloadHtml;
		[NullAllowed, Export ("payloadHtml")]
		string PayloadHtml { get; }

		// @property (readonly, nonatomic) BOOL isHtml;
		[Export ("isHtml")]
		bool IsHtml { get; }

		// @property (readonly, nonatomic) BOOL rawHasTrackingConsent;
		[Export ("rawHasTrackingConsent")]
		bool RawHasTrackingConsent { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable consentCategoryTracking;
		[NullAllowed, Export ("consentCategoryTracking")]
		string ConsentCategoryTracking { get; }

		// -(instancetype _Nonnull)initWithId:(NSString * _Nonnull)id name:(NSString * _Nonnull)name rawMessageType:(NSString * _Nonnull)rawMessageType variantId:(NSInteger)variantId variantName:(NSString * _Nonnull)variantName rawFrequency:(NSString * _Nonnull)rawFrequency eventType:(NSString * _Nonnull)eventType priority:(NSInteger)priority delayMS:(NSInteger)delayMS timeoutMS:(NSInteger)timeoutMS payloadHtml:(NSString * _Nullable)payloadHtml isHtml:(BOOL)isHtml rawHasTrackingConsent:(BOOL)rawHasTrackingConsent consentCategoryTracking:(NSString * _Nullable)consentCategoryTracking __attribute__((objc_designated_initializer));
		[Export ("initWithId:name:rawMessageType:variantId:variantName:rawFrequency:eventType:priority:delayMS:timeoutMS:payloadHtml:isHtml:rawHasTrackingConsent:consentCategoryTracking:")]
		[DesignatedInitializer]
		IntPtr Constructor(string id, string name, string rawMessageType, int variantId, string variantName, string rawFrequency, string eventType, int priority, int delayMS, int timeoutMS, string payloadHtml, [NullAllowed] bool isHtml, bool rawHasTrackingConsent, [NullAllowed] string consentCategoryTracking);

	}

	// @interface XamarinAuthorizationProvider : NSObject
	[BaseType(typeof(NSObject))]
	interface XamarinAuthorizationProvider
	{
		// -(NSString * _Nullable)getAuthorizationToken __attribute__((warn_unused_result("")));
		[NullAllowed, Export("getAuthorizationToken")]
		string AuthorizationToken { get; }
	}
}
