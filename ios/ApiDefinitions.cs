using System;
using Foundation;
using ObjCRuntime;

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
		void ConfigureWithConfiguration (NSDictionary configuration);

		// -(void)trackEventWithEventType:(NSString * _Nonnull)eventType properties:(NSDictionary * _Nonnull)properties timestamp:(double)timestamp;
		[Export ("trackEventWithEventType:properties:timestamp:")]
		void TrackEventWithEventType (string eventType, NSDictionary properties, double timestamp);

		// -(void)anonymizeWithExponeaProjectDictionary:(NSDictionary * _Nullable)exponeaProjectDictionary projectMappingDictionary:(NSDictionary * _Nullable)projectMappingDictionary;
		[Export ("anonymizeWithExponeaProjectDictionary:projectMappingDictionary:")]
		void AnonymizeWithExponeaProjectDictionary ([NullAllowed] NSDictionary exponeaProjectDictionary, [NullAllowed] NSDictionary projectMappingDictionary);

		// -(void)anonymize;
		[Export ("anonymize")]
		void Anonymize ();

		// -(void)flushData;
		[Export ("flushData")]
		void FlushData ();

		// -(void)flushDataWithDone:(void (^ _Nonnull)(NSString * _Nonnull))done;
		[Export ("flushDataWithDone:")]
		void FlushDataWithDone (Action<NSString> done);

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
		void SetFlushModeWithFlushMode (string flushMode);

		// -(NSInteger)getFlushPeriod __attribute__((warn_unused_result("")));
		[Export ("getFlushPeriod")]
		nint FlushPeriod { get; }

		// -(void)setFlushPeriodWithFlushPeriod:(NSNumber * _Nonnull)flushPeriod;
		[Export ("setFlushPeriodWithFlushPeriod:")]
		void SetFlushPeriodWithFlushPeriod (NSNumber flushPeriod);

		// -(NSString * _Nonnull)getLogLevel __attribute__((warn_unused_result("")));
		[Export ("getLogLevel")]
		string LogLevel { get; }

		// -(void)setLogLevelWithLogLevel:(NSString * _Nonnull)logLevel;
		[Export ("setLogLevelWithLogLevel:")]
		void SetLogLevelWithLogLevel (string logLevel);

		// -(NSString * _Nonnull)getDefaultProperties __attribute__((warn_unused_result("")));
		[Export ("getDefaultProperties")]
		string DefaultProperties { get; }

		// -(void)setDefaultPropertiesWithProperties:(NSDictionary * _Nonnull)properties;
		[Export ("setDefaultPropertiesWithProperties:")]
		void SetDefaultPropertiesWithProperties (NSDictionary properties);

		// -(void)identifyCustomerWithCustomerIds:(NSDictionary * _Nonnull)customerIds properties:(NSDictionary * _Nonnull)properties;
		[Export ("identifyCustomerWithCustomerIds:properties:")]
		void IdentifyCustomerWithCustomerIds (NSDictionary customerIds, NSDictionary properties);

		// -(void)trackSessionStart;
		[Export ("trackSessionStart")]
		void TrackSessionStart ();

		// -(void)trackSessionEnd;
		[Export ("trackSessionEnd")]
		void TrackSessionEnd ();

		// -(void)trackPushOpenedWithUserInfo:(NSDictionary * _Nonnull)userInfo;
		[Export ("trackPushOpenedWithUserInfo:")]
		void TrackPushOpenedWithUserInfo (NSDictionary userInfo);

		// -(void)trackPaymentWithProperties:(NSDictionary * _Nonnull)properties timestamp:(double)timestamp;
		[Export ("trackPaymentWithProperties:timestamp:")]
		void TrackPaymentWithProperties (NSDictionary properties, double timestamp);

		// -(void)fetchConsentsWithSuccess:(void (^ _Nonnull)(NSString * _Nonnull))success fail:(void (^ _Nonnull)(NSString * _Nonnull))fail;
		[Export ("fetchConsentsWithSuccess:fail:")]
		void FetchConsentsWithSuccess (Action<NSString> success, Action<NSString> fail);

		// -(void)fetchRecommendationsWithOptionsDictionary:(NSDictionary * _Nonnull)optionsDictionary success:(void (^ _Nonnull)(NSString * _Nonnull))success fail:(void (^ _Nonnull)(NSString * _Nonnull))fail;
		[Export ("fetchRecommendationsWithOptionsDictionary:success:fail:")]
		void FetchRecommendationsWithOptionsDictionary (NSDictionary optionsDictionary, Action<NSString> success, Action<NSString> fail);
	}
}
