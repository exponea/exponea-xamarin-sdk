using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Exponea.Sdk.Util;
using Exponea.Android;
using ExponeaSdkAndroid = ExponeaSdk.Models;
using Essentials = Xamarin.Essentials;
using AndroidApp = Android.App;
using Result = ExponeaSdk.Models.Result;
using Newtonsoft.Json;
using Com.Exponea.Style;
using Com.Exponea.Sdk.Style.Appinbox;
using NativeAndroid = Android.Widget;
using AndroidForms = Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Google.Gson.Internal;

namespace Exponea
{
    public class ExponeaSdk : IExponeaSdk
    {
        public static readonly global::ExponeaSdk.Exponea _exponea = global::ExponeaSdk.Exponea.Instance;

        public ExponeaSdk()
        {

        }

        public void Configure(Configuration source)
        {
            var target = new ExponeaSdkAndroid.ExponeaConfiguration
            {
                ProjectToken = source.ProjectToken,
                Authorization = "Token " + source.Authorization,
                BaseURL = source.BaseUrl,

            };

            if (source.AutomaticSessionTracking != null) {
                target.AutomaticSessionTracking = (bool)source.AutomaticSessionTracking;
            }
            if (source.DefaultProperties != null)
            {
                target.DefaultProperties = source.DefaultProperties.ToJavaDictionary();
            }
            if (source.MaxTries != null)
            {
                target.MaxTries = (int)source.MaxTries;
            }
            if (source.SessionTimeout != null)
            {
                target.SessionTimeout = (double)source.SessionTimeout;
            }
            if (source.ProjectRouteMap != null)
            {
                target.ProjectRouteMap = source.ProjectRouteMap.ToJavaDictionary();
            }

            target.TokenTrackFrequency = global::ExponeaSdk.Models.ExponeaConfiguration.TokenFrequency.ValueOf(
                    source.TokenTrackFrequency.ToJavaEnumName<TokenTrackFrequencyInternal, TokenTrackFrequency>()
            );

            if (source.AndroidConfiguration != null)
            {
                var androidConfig = source.AndroidConfiguration;
                target.AutomaticPushNotification = androidConfig.AutomaticPushNotification;
                if (androidConfig.PushAccentColor != null)
                {
                    target.PushAccentColor = new Java.Lang.Integer((int)androidConfig.PushAccentColor);
                }
                if (androidConfig.PushIcon != null)
                {
                    target.PushIcon = Utils.GetResourceId(AndroidApp.Application.Context, androidConfig.PushIcon);
                }
                if (androidConfig.PushChannelDescription != null)
                {
                    target.PushChannelDescription = (string)androidConfig.PushChannelDescription;
                }
                if (androidConfig.PushChannelName != null)
                {
                    target.PushChannelName = (string)androidConfig.PushChannelName;
                }
                if (androidConfig.PushChannelId != null)
                {
                    target.PushChannelId = (string)androidConfig.PushChannelId;
                }
                if (androidConfig.PushNotificationImportance != null)
                {
                    target.PushNotificationImportance = (int)androidConfig.PushNotificationImportance;
                }
            }

            target.AllowDefaultCustomerProperties = source.AllowDefaultCustomerProperties;
            target.AdvancedAuthEnabled = source.AdvancedAuthEnabled;

            _exponea.Init(Essentials.Platform.CurrentActivity, target);
        }

        public bool IsConfigured
        {
            get => _exponea.IsInitialized;
        }

        public void SwitchProject(Project project, IDictionary<EventType, IList<Project>> projectMapping = null)
        {
            _exponea.Anonymize(new ExponeaSdkAndroid.ExponeaProject(project.BaseUrl, project.ProjectToken, "Token" + project.Authorization), projectMapping.ToJavaDictionary());
        }

        public string CustomerCookie
            => _exponea.CustomerCookie;

        public bool AutomaticSessionTracking
        {
            get => _exponea.AutomaticSessionTracking;
            set => _exponea.AutomaticSessionTracking = value;
        }

        public FlushMode FlushMode
        {
            get => _exponea.FlushMode.ToNetEnum<FlushMode, FlushModeInternal>();
            set => _exponea.FlushMode = global::ExponeaSdk.Models.FlushMode.ValueOf(value.ToJavaEnumName<FlushModeInternal, FlushMode>());
        }

        public TimeSpan FlushPeriod
        {
            get => _exponea.FlushPeriod.ToTimeSpan();
            set => _exponea.FlushPeriod = value.ToFlushPeriod();
        }

        public LogLevel LogLevel
        {
            get => _exponea.LoggerLevel.ToNetEnum<LogLevel, LogLevelInternal>();
            set => _exponea.LoggerLevel = Logger.Level.ValueOf(value.ToJavaEnumName<LogLevelInternal, LogLevel>());
        }

        public IDictionary<string, object> GetDefaultProperties()
            => _exponea.DefaultProperties.ToNetDictionary();

        public void SetDefaultProperties(IDictionary<string, object> properties)
            => _exponea.DefaultProperties = properties.ToJavaDictionary();

        public void Anonymize() => _exponea.Anonymize();

        public void IdentifyCustomer(Customer customer)
            => _exponea.IdentifyCustomer(
                new ExponeaSdkAndroid.CustomerIds(customer.ExternalIds),
                new ExponeaSdkAndroid.PropertiesList(customer.Attributes.ToJavaDictionary()));

        public void Track(Click click)
        {
            var consentCategoryTracking = click.Attributes.GetValueOrDefault("consent_category_tracking");
            var hasTrackingConsent = click.Attributes.GetValueOrDefault("has_tracking_consent");
            _exponea.TrackClickedPush(
                new ExponeaSdkAndroid.NotificationData(
                    click.Attributes.ToJavaDictionary(),
                    new ExponeaSdkAndroid.CampaignData(),
                    consentCategoryTracking == null ? null : (string)consentCategoryTracking,
                    Com.Exponea.Sdk.Util.GdprTracking.Instance.HasTrackingConsent(
                        hasTrackingConsent.ToJava()
                    )
                ),
                new ExponeaSdkAndroid.NotificationAction(click.ActionType, click.ActionName, click.Url),
                Utils.GetTimestamp()
            );
        }

        public void TrackWithoutTrackingConsent(Click click)
        {
            var consentCategoryTracking = click.Attributes.GetValueOrDefault("consent_category_tracking");
            var hasTrackingConsent = click.Attributes.GetValueOrDefault("has_tracking_consent");
            _exponea.TrackClickedPushWithoutTrackingConsent(
                new ExponeaSdkAndroid.NotificationData(
                    click.Attributes.ToJavaDictionary(),
                    new ExponeaSdkAndroid.CampaignData(),
                    consentCategoryTracking == null ? null : (string)consentCategoryTracking,
                    Com.Exponea.Sdk.Util.GdprTracking.Instance.HasTrackingConsent(
                        hasTrackingConsent.ToJava()
                    )
                ),
                new ExponeaSdkAndroid.NotificationAction(click.ActionType, click.ActionName, click.Url),
                (Java.Lang.Double)Utils.GetTimestamp()
            );
        }

        public void Track(Delivery delivery)
        {
            var consentCategoryTracking = delivery.Attributes.GetValueOrDefault("consent_category_tracking");
            var hasTrackingConsent = delivery.Attributes.GetValueOrDefault("has_tracking_consent");
            _exponea.TrackDeliveredPush(
                new ExponeaSdkAndroid.NotificationData(
                    delivery.Attributes.ToJavaDictionary(),
                    new ExponeaSdkAndroid.CampaignData(),
                    consentCategoryTracking == null ? null : (string)consentCategoryTracking,
                    Com.Exponea.Sdk.Util.GdprTracking.Instance.HasTrackingConsent(
                        hasTrackingConsent.ToJava()
                    )
                ),
                Utils.GetTimestamp()
            );
        }

        public void Track(Event evt, double? timestamp = null)
            => _exponea.TrackEvent(
                new ExponeaSdkAndroid.PropertiesList(evt.Attributes.ToJavaDictionary()),
                timestamp != null ? (double)timestamp : Utils.GetTimestamp(),
                evt.Name);

        public void Track(Payment payment, double? timestamp = null)
            => _exponea.TrackPaymentEvent(
                timestamp != null ? (double)timestamp : Utils.GetTimestamp(),
                new ExponeaSdkAndroid.PurchasedItem((double)payment.Value, payment.Currency, payment.System, payment.ProductId, payment.ProductTitle, payment.Receipt));

        public Task FlushAsync()
        {
            var tcs = new TaskCompletionSource<Kotlin.Result>();
            _exponea.FlushData(new KotlinCallback<Kotlin.Result>(tcs.SetResult));
            return tcs.Task;
        }

        public void Flush()
        {
            _exponea.FlushData();
        }

        public Task<string> FetchConsentsAsync()
        {
            var tcs = new TaskCompletionSource<string>();
            _exponea.GetConsents(
                new KotlinCallback<Result>(r =>
                {

                    tcs.SetResult(r.ToString());
                }),
                new KotlinCallback<Result>(r =>
                {
                    var err = (ExponeaSdkAndroid.FetchError)r.Results;
                    tcs.SetException(new FetchException(err.Message, err.JsonBody));
                }));
            return tcs.Task;
        }

        public Task<string> FetchRecommendationsAsync(RecommendationsRequest request)
        {
            var tcs = new TaskCompletionSource<string>();
            var recommendationOptions = new ExponeaSdkAndroid.CustomerRecommendationOptions(
                request.Id,
                request.FillWithRandom,
                request.Size,
                request.Items,
                Java.Lang.Boolean.ValueOf(request.NoTrack),
                request.CatalogAttributesWhitelist);
            _exponea.FetchRecommendation(recommendationOptions,
                new KotlinCallback<Result>(r =>
                {
                    //TODO: Return list of CustomerRecommendation instead of string
                    tcs.SetResult(r.ToString());
                }),
                new KotlinCallback<Result>(r =>
                {
                    var err = (ExponeaSdkAndroid.FetchError)r.Results;
                    tcs.SetException(new FetchException(err.Message, err.JsonBody));
                }));
            return tcs.Task;
        }

        public void TrackSessionStart()
        {
            _exponea.TrackSessionStart(Utils.GetTimestamp());
        }

        public void TrackSessionEnd()
        {
            _exponea.TrackSessionEnd(Utils.GetTimestamp());
        }

        public void TrackPushToken(string token)
        {
            _exponea.TrackPushToken(token);
        }

        public void CheckPushSetup()
        {
            _exponea.CheckPushSetup = true;
        }

        public void SetInAppMessageDelegate(bool overrideDefaultBehavior, bool trackActions, Action<InAppMessage, string , string, bool> action)
        {
            _exponea.InAppMessageActionCallback = new InAppMessageCallback(overrideDefaultBehavior, trackActions, action);
        }

        public void TrackInAppMessageClick(InAppMessage message, string buttonText, string buttonLink)
        {
            _exponea.TrackInAppMessageClick(message.ToAndroidInAppMessage(), buttonText, buttonLink);
        }

        public void TrackInAppMessageClose(InAppMessage message)
        {
            _exponea.TrackInAppMessageClose(message.ToAndroidInAppMessage(), Java.Lang.Boolean.True);
        }

        public void TrackInAppMessageClose(InAppMessage message, bool userInteraction)
        {
            _exponea.TrackInAppMessageClose(message.ToAndroidInAppMessage(), (Java.Lang.Boolean)userInteraction.ToJava());
        }

        public void SetAppInboxProvider(IDictionary<string, object> data)
        {
            AppInboxStyle appInboxStyle = new AppInboxStyleParser(data.ToJavaDictionary()).Parse();
            _exponea.AppInboxProvider = new StyledAppInboxProvider(appInboxStyle);
        }

        public AndroidForms.View GetAppInboxButton()
        {
            AndroidForms.ContentView wrapper = new AndroidForms.ContentView();
            AndroidForms.StackLayout stackLayout = new AndroidForms.StackLayout();
            wrapper.Content = stackLayout;
            NativeAndroid.Button button = (NativeAndroid.Button)_exponea.GetAppInboxButton(AndroidApp.Application.Context);
            stackLayout.Children.Add(button);
            return wrapper;
        }

        void IExponeaSdk.TrackWithoutTrackingConsent(Delivery delivery)
        {
            var consentCategoryTracking = delivery.Attributes.GetValueOrDefault("consent_category_tracking");
            var hasTrackingConsent = delivery.Attributes.GetValueOrDefault("has_tracking_consent");
            _exponea.TrackDeliveredPushWithoutTrackingConsent(
                new ExponeaSdkAndroid.NotificationData(
                    delivery.Attributes.ToJavaDictionary(),
                    new ExponeaSdkAndroid.CampaignData(),
                    consentCategoryTracking == null ? null : (string)consentCategoryTracking,
                    Com.Exponea.Sdk.Util.GdprTracking.Instance.HasTrackingConsent(
                        hasTrackingConsent.ToJava()
                    )
                ),
                Utils.GetTimestamp()
            );
        }

        void IExponeaSdk.TrackWithoutTrackingConsent(Click click)
        {
            var consentCategoryTracking = click.Attributes.GetValueOrDefault("consent_category_tracking");
            var hasTrackingConsent = click.Attributes.GetValueOrDefault("has_tracking_consent");
            _exponea.TrackClickedPushWithoutTrackingConsent(
                new ExponeaSdkAndroid.NotificationData(
                    click.Attributes.ToJavaDictionary(),
                    new ExponeaSdkAndroid.CampaignData(),
                    consentCategoryTracking == null ? null : (string)consentCategoryTracking,
                    Com.Exponea.Sdk.Util.GdprTracking.Instance.HasTrackingConsent(
                        hasTrackingConsent.ToJava()
                    )
                ),
                new ExponeaSdkAndroid.NotificationAction(click.ActionType, click.ActionName, click.Url),
                new Java.Lang.Double(Utils.GetTimestamp())
            );
        }

        void IExponeaSdk.HandlePushNotificationOpened(Click click, string actionIdentifier)
        {
            // TODO: Android works differently from iOS, not API exposed, just tracking
            this.Track(click);
        }

        void IExponeaSdk.HandlePushNotificationOpenedWithoutTrackingConsent(Click click, string actionIdentifier)
        {
            // TODO: Android works differently from iOS, not API exposed, just tracking
            this.TrackWithoutTrackingConsent(click);
        }

        void IExponeaSdk.TrackCampaign(Uri url, double? timestamp)
        {
            var campaignIntent = new global::Android.Content.Intent();
            campaignIntent.SetData(global::Android.Net.Uri.Parse(url.AbsoluteUri));
            _exponea.HandleCampaignIntent(campaignIntent, AndroidApp.Application.Context);
        }

        void IExponeaSdk.HandlePushToken(string token)
        {
            _exponea.HandleNewToken(AndroidApp.Application.Context, token);
        }

        void IExponeaSdk.HandleHmsPushToken(string token)
        {
            _exponea.HandleNewHmsToken(AndroidApp.Application.Context, token);
        }

        void IExponeaSdk.TrackInAppMessageClickWithoutTrackingConsent(InAppMessage message, string buttonText, string buttonLink)
        {
            _exponea.TrackInAppMessageClickWithoutTrackingConsent(message.ToAndroidInAppMessage(), buttonText, buttonLink);
        }

        void IExponeaSdk.TrackInAppMessageClose(InAppMessage message, bool? isUserInteraction)
        {
            _exponea.TrackInAppMessageClose(message.ToAndroidInAppMessage(), isUserInteraction.ToJava());
        }

        void IExponeaSdk.TrackInAppMessageCloseWithoutTrackingConsent(InAppMessage message, bool? isUserInteraction)
        {
            _exponea.TrackInAppMessageClose(message.ToAndroidInAppMessage(), isUserInteraction.ToJava());
        }

        void IExponeaSdk.TrackAppInboxOpened(AppInboxMessage message)
        {
            _exponea.TrackAppInboxOpened(message.ToNative());
        }

        void IExponeaSdk.TrackAppInboxOpenedWithoutTrackingConsent(AppInboxMessage message)
        {
            _exponea.TrackAppInboxOpenedWithoutTrackingConsent(message.ToNative());
        }

        void IExponeaSdk.TrackAppInboxClick(AppInboxAction action, AppInboxMessage message)
        {
            _exponea.TrackAppInboxClick(action.ToJavaAppInboxAction(), message.ToNative());
        }

        void IExponeaSdk.TrackAppInboxClickWithoutTrackingConsent(AppInboxAction action, AppInboxMessage message)
        {
            _exponea.TrackAppInboxClickWithoutTrackingConsent(action.ToJavaAppInboxAction(), message.ToNative());
        }

        Task<bool> IExponeaSdk.MarkAppInboxAsRead(AppInboxMessage message)
        {
            var tcs = new TaskCompletionSource<bool>();
            _exponea.MarkAppInboxAsRead(
                message.ToNative(),
                new KotlinCallback<Java.Lang.Boolean>(r =>
                {
                    tcs.SetResult((bool)r.ToNet());
                })
            );
            return tcs.Task;
        }

        Task<IList<AppInboxMessage>> IExponeaSdk.FetchAppInbox()
        {
            var tcs = new TaskCompletionSource<IList<AppInboxMessage>>();
            _exponea.FetchAppInbox(new KotlinCallback<Java.Lang.Object>(nativeR =>
            {
                var result = new List<AppInboxMessage>();
                Console.WriteLine("APP_INBOX got response " + nativeR.ToString());
                if (nativeR != null)
                {
                    var list = (Java.Util.ICollection)nativeR;
                    Console.WriteLine("APP_INBOX of size " + list.Size());
                    foreach (var item in list.ToArray())
                    {
                        var typedItem = (ExponeaSdkAndroid.MessageItem)item;
                        Console.WriteLine("APP_INBOX got native type ");
                        result.Add(typedItem.ToNet());
                    }
                }
                tcs.SetResult(result);
            }));
            return tcs.Task;
        }

        Task<AppInboxMessage> IExponeaSdk.FetchAppInboxItem(string messageId)
        {
            var tcs = new TaskCompletionSource<AppInboxMessage>();
            _exponea.FetchAppInboxItem(messageId, new KotlinCallback<ExponeaSdkAndroid.MessageItem>(nativeR =>
            {
                tcs.SetResult(nativeR.ToNet());
            }));
            return tcs.Task;
        }
    }
}
