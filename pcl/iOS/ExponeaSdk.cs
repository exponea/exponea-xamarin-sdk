using Exponea.iOS;
using Foundation;
using System;
using System.Collections.Generic;
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
            => throw new NotSupportedException();

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

        public void TrackInAppMessageClose(InAppMessage message)
        {
            _exponea.TrackInAppMessageClose(message.ToNsSimleInAppMessage());
        }

        public void SetAppInboxProvider(IDictionary<string, object> data)
        {
            _exponea.setAppInboxProvider(data.ToNsDictionary());
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
            throw new NotSupportedException();
        }

        void IExponeaSdk.TrackWithoutTrackingConsent(Click click)
        {
            throw new NotImplementedException();
        }

        void IExponeaSdk.HandlePushNotificationOpened(Click click, string actionIdentifier)
        {
            throw new NotImplementedException();
        }

        void IExponeaSdk.HandlePushNotificationOpenedWithoutTrackingConsent(Click click, string actionIdentifier)
        {
            throw new NotImplementedException();
        }

        void IExponeaSdk.TrackCampaign(Uri url, double? timestamp)
        {
            // shoud be _exponea.TrackCampaignClick(Url, double)
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        void IExponeaSdk.TrackInAppMessageClose(InAppMessage message, bool? isUserInteraction)
        {
            throw new NotImplementedException();
        }

        void IExponeaSdk.SetPushNotificationsDelegate(Action<NotificationActionType, string, IDictionary<string, object>> action)
        {
            throw new NotImplementedException();
        }

        void IExponeaSdk.TrackAppInboxOpened(AppInboxMessage message)
        {
            throw new NotImplementedException();
        }

        void IExponeaSdk.TrackAppInboxOpenedWithoutTrackingConsent(AppInboxMessage message)
        {
            throw new NotImplementedException();
        }

        void IExponeaSdk.TrackAppInboxClick(AppInboxAction action, AppInboxMessage message)
        {
            throw new NotImplementedException();
        }

        void IExponeaSdk.TrackAppInboxClickWithoutTrackingConsent(AppInboxAction action, AppInboxMessage message)
        {
            throw new NotImplementedException();
        }

        Task<IList<AppInboxMessage>> IExponeaSdk.FetchAppInbox()
        {
            throw new NotImplementedException();
        }

        Task<AppInboxMessage> IExponeaSdk.FetchAppInboxItem(string messageId)
        {
            throw new NotImplementedException();
        }

        public void TrackInAppMessageCloseWithoutTrackingConsent(InAppMessage message, bool? isUserInteraction = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkAppInboxAsRead(AppInboxMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
