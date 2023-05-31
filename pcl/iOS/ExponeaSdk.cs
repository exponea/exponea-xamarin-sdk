using Exponea.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ExponeaSdkIos = ExponeaSdk;


namespace Exponea
{
    public class ExponeaSdk : IExponeaSdk
    {
        private readonly ExponeaSdkIos.Exponea _exponea = ExponeaSdkIos.Exponea.Instance;

        public string CustomerCookie => _exponea.CustomerCookie;

        public bool IsConfigured
        {
            get => _exponea.IsConfigured;
        }

        public bool AutomaticSessionTracking
        {
            get => _exponea.IsAutoSessionTrackingEnabled;
            set => throw new NotSupportedException();
        }

        public FlushMode FlushMode
        {
            get => (FlushMode)(Enum.TryParse<FlushModeInternal>(_exponea.FlushMode, true, out var value) ? value : default);
            set => _exponea.SetFlushMode(((FlushModeInternal)value).ToString());
        }

        public TimeSpan FlushPeriod
        {
            get => TimeSpan.FromMilliseconds(_exponea.FlushPeriod);
            set => _exponea.SetFlushPeriod(value.TotalMilliseconds);
        }

        public LogLevel LogLevel
        {
            get => (LogLevel)(Enum.TryParse<LogLevelInternal>(_exponea.LogLevel, true, out var value) ? value : default);
            set => _exponea.SetLogLevel(((LogLevelInternal)value).ToString());
        }

        public void Anonymize()
        {

            _exponea.Anonymize();
        }

        public void Configure(Configuration config)
        {
            _exponea.Configure(config.ToNSDictionary());
        }

        public void SwitchProject(Project project, IDictionary<EventType, IList<Project>> projectMapping = null)
        {
            _exponea.Anonymize(project.ToNsDictionary(), projectMapping.ToNsDictionary());
        }

        public Task<string> FetchConsentsAsync()
        {
            var tcs = new TaskCompletionSource<string>();
            _exponea.FetchConsents(
                success => tcs.SetResult(success),
                failure => tcs.SetException(new FetchException(failure, failure)));
            return tcs.Task;
        }

        public Task<string> FetchRecommendationsAsync(RecommendationsRequest request)
        {
            var tcs = new TaskCompletionSource<string>();

            var options = new NSDictionary(
                "id", request.Id.ToNSObject(),
                "fillWithRandom", request.FillWithRandom.ToNSObject(),
                "size", request.Size.ToNSObject(),
                "items", request.Items.ToNsDictionary<string>(),
                "noTrack", request.NoTrack.ToNSObject(),
                "catalogAttributesWhitelist", request.CatalogAttributesWhitelist.ToNSArray<string>());

            Action<NSString> successDelegate = delegate (NSString success)
            {
                //TODO: Parse result as list of CustomerRecommendation
                tcs.SetResult(success);
            };

            Action<NSString> failDelegate = delegate (NSString error)
            {
                tcs.SetException(new FetchException(error, error));
            };

            _exponea.FetchRecommendations(options, successDelegate, failDelegate);

            return tcs.Task;
        }

        public Task FlushAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            _exponea.FlushData(done => tcs.SetResult(true));
            return tcs.Task;
        }

        public void Flush()
        {
            _exponea.FlushData();
        }

        public IDictionary<string, object> GetDefaultProperties()
        {
            if (_exponea.DefaultProperties == null)
            {
                return new Dictionary<string, object>();
            }

            try
            {
                return JsonSerializer.Deserialize<IDictionary<string, object>>(_exponea.DefaultProperties);
            }
            catch
            {
                return new Dictionary<string, object>();
            }
        }

        public void SetDefaultProperties(IDictionary<string, object> properties)
        {
            _exponea.SetDefaultProperties(properties.ToNsDictionary<Object>());
        }

        public void IdentifyCustomer(Customer customer)
        {
            _exponea.IdentifyCustomer(customer.ExternalIds.ToNsDictionary(), customer.Attributes.ToNsDictionary());
        }

        public void Track(Event evt, double? timestamp = null)
            => _exponea.TrackEvent(
                evt.Name,
                evt.Attributes.ToNsDictionary(),
                timestamp != null ? (double)timestamp : Utils.GetTimestamp());

        public void Track(Delivery delivery)
        {
            _exponea.TrackPushReceived(delivery.Attributes.ToNsDictionary());
        }

        public void Track(Click click)
        {
            var info = click.Attributes.ToNsDictionary();
            if (click.ActionType != null) { info["action_type"] = click.ActionType.ToNsString(); }
            if (click.ActionName != null) { info["action_name"] = click.ActionName.ToNsString(); }
            if (click.Url != null) { info["url"] = click.Url.ToNsString(); }
            _exponea.TrackPushOpened(info);
        }

        public void Track(Payment payment, double? timestamp = null)
        {
            _exponea.TrackPayment(payment.ToNsDictionary(), timestamp != null ? (double)timestamp : Utils.GetTimestamp());
        }

        public void TrackPushToken(string token)
        {
           _exponea.TrackPushToken(token);
        }

        public void TrackSessionStart()
        {
            _exponea.TrackSessionStart();
        }

        public void TrackSessionEnd()
        {
            _exponea.TrackSessionEnd();
        }

        public void CheckPushSetup()
        {
            _exponea.CheckPushSetup();
        }

        public void SetInAppMessageDelegate(bool overrideDefaultBehavior, bool trackActions, Action<InAppMessage, string, string, bool> action)
        {
            _exponea.SetInAppMessageDelegate(overrideDefaultBehavior, trackActions, delegate (ExponeaSdkIos.SimpleInAppMessage message, NSString buttonText, NSString buttonUrl, bool interaction)
            {
                action.Invoke(message.ToNetInAppMessage(), buttonText, buttonUrl, interaction);
            });
        }

        public void TrackInAppMessageClick(InAppMessage message, string buttonText, string buttonLink)
        {
            _exponea.TrackInAppMessageClick(message.ToNsSimleInAppMessage(), buttonText, buttonLink);
        }

        public void TrackInAppMessageClose(InAppMessage message, bool? isUserInteraction)
        {
            _exponea.TrackInAppMessageClose(message.ToNsSimleInAppMessage(), isUserInteraction ?? true);
        }

        public void TrackInAppMessageCloseWithoutTrackingConsent(InAppMessage message, bool? isUserInteraction)
        {
            _exponea.TrackInAppMessageCloseWithoutTrackingConsent(message.ToNsSimleInAppMessage(), isUserInteraction ?? true);
        }

        public void SetAppInboxProvider(IDictionary<string, object> data)
        {
            _exponea.SetAppInboxProviderWithData(data.ToNsDictionary());
        }

        public View GetAppInboxButton()
        {
            ContentView wrapper = new ContentView();
            StackLayout stackLayout = new StackLayout();
            wrapper.Content = stackLayout;
            UIButton button = (UIButton) _exponea.getAppInboxButton();
            stackLayout.Children.Add(button);
            return wrapper;
        }

        void IExponeaSdk.TrackWithoutTrackingConsent(Delivery delivery)
        {
            _exponea.TrackPushReceivedWithoutTrackingConsent(delivery.Attributes.ToNsDictionary());
        }

        void IExponeaSdk.TrackWithoutTrackingConsent(Click click)
        {
            var info = click.Attributes.ToNsDictionary();
            if (click.ActionType != null) { info["action_type"] = click.ActionType.ToNsString(); }
            if (click.ActionName != null) { info["action_name"] = click.ActionName.ToNsString(); }
            if (click.Url != null) { info["url"] = click.Url.ToNsString(); }
            _exponea.TrackPushOpenedWithoutTrackingConsent(info);
        }

        void IExponeaSdk.HandlePushNotificationOpened(Click click, string actionIdentifier)
        {
            _exponea.HandlePushNotificationOpened(click.Attributes.ToNsDictionary(), actionIdentifier);
        }

        void IExponeaSdk.HandlePushNotificationOpenedWithoutTrackingConsent(Click click, string actionIdentifier)
        {
            _exponea.HandlePushNotificationOpenedWithoutTrackingConsentWithUserInfo(click.Attributes.ToNsDictionary(), actionIdentifier);
        }

        void IExponeaSdk.TrackCampaign(Uri url, double? timestamp)
        {
            _exponea.HandleCampaignClick(url, timestamp != null ? (double)timestamp : Utils.GetTimestamp());
        }

        void IExponeaSdk.HandlePushToken(string token)
        {
            _exponea.HandlePushNotificationToken(new NSData());
        }

        void IExponeaSdk.HandleHmsPushToken(string token)
        {
            throw new NotSupportedException();
        }

        void IExponeaSdk.TrackInAppMessageClickWithoutTrackingConsent(InAppMessage message, string buttonText, string buttonLink)
        {
            _exponea.TrackInAppMessageClickWithoutTrackingConsent(message.ToNsSimleInAppMessage(), buttonText, buttonLink);
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
            _exponea.TrackAppInboxClick(action.ToNative(), message.ToNative());
        }

        void IExponeaSdk.TrackAppInboxClickWithoutTrackingConsent(AppInboxAction action, AppInboxMessage message)
        {
            _exponea.TrackAppInboxClickWithoutTrackingConsent(action.ToNative(), message.ToNative());
        }

        Task<IList<AppInboxMessage>> IExponeaSdk.FetchAppInbox()
        {
            var tcs = new TaskCompletionSource<IList<AppInboxMessage>>();
            _exponea.FetchAppInbox(
                success => {
                    var response = new List<AppInboxMessage>();
                    foreach (var each in success.ToArray())
                    {
                        response.Add(each.ToNet());
                    }
                    tcs.SetResult(response);
                },
                failure => tcs.SetException(new FetchException(failure, failure)));
            return tcs.Task;
        }

        Task<AppInboxMessage> IExponeaSdk.FetchAppInboxItem(string messageId)
        {
            var tcs = new TaskCompletionSource<AppInboxMessage>();
            _exponea.FetchAppInboxItemWithMessageId(
                messageId,
                success => tcs.SetResult(success.ToNet()),
                failure => tcs.SetException(new FetchException(failure, failure))
            );
            return tcs.Task;
        }

        public Task<bool> MarkAppInboxAsRead(AppInboxMessage message)
        {
            var tcs = new TaskCompletionSource<bool>();
            _exponea.MarkAppInboxAsRead(
                message.ToNative(),
                success => tcs.SetResult(success),
                failure => tcs.SetException(new FetchException(failure, failure))
            );
            return tcs.Task;
        }
    }
}
