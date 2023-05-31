using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exponea
{
    public interface IExponeaSdk
    {
        void Configure(Configuration config);
        bool IsConfigured { get; }
        void SwitchProject(Project project, IDictionary<EventType, IList<Project>> projectMapping = null);

        void Track(Event evt, double? timestamp = null);
        void Track(Delivery delivery);
        void TrackWithoutTrackingConsent(Delivery delivery);
        void Track(Click click);
        void TrackWithoutTrackingConsent(Click click);
        void HandlePushNotificationOpened(Click click, string actionIdentifier);
        void HandlePushNotificationOpenedWithoutTrackingConsent(Click click, string actionIdentifier);
        void TrackCampaign(Uri url, double? timestamp = null);
        void Track(Payment payment, double? timestamp = null);
        void TrackPushToken(string token);
        void HandlePushToken(string token);
        void HandleHmsPushToken(string token);
        void TrackSessionStart();
        void TrackSessionEnd();
        void Anonymize();
        void IdentifyCustomer(Customer customer);

        string CustomerCookie { get; }

        Task FlushAsync();
        void Flush();
        Task<string> FetchConsentsAsync();
        Task<string> FetchRecommendationsAsync(RecommendationsRequest request);

        bool AutomaticSessionTracking { get; set; }
        FlushMode FlushMode { get; set; }
        TimeSpan FlushPeriod { get; set; }
        LogLevel LogLevel { get; set; }
        IDictionary<string, object> GetDefaultProperties();
        void SetDefaultProperties(IDictionary<string, object> properties);
        void CheckPushSetup();
        void SetInAppMessageDelegate(bool overrideDefaultBehavior, bool trackActions, Action<InAppMessage, string, string, bool> action);
        void TrackInAppMessageClick(InAppMessage message, string buttonText, string buttonLink);
        void TrackInAppMessageClickWithoutTrackingConsent(InAppMessage message, string buttonText, string buttonLink);
        void TrackInAppMessageClose(InAppMessage message, bool? isUserInteraction = null);
        void TrackInAppMessageCloseWithoutTrackingConsent(InAppMessage message, bool? isUserInteraction = null);
        void SetAppInboxProvider(IDictionary<string, object> data);
        View GetAppInboxButton();
        void TrackAppInboxOpened(AppInboxMessage message);
        void TrackAppInboxOpenedWithoutTrackingConsent(AppInboxMessage message);
        void TrackAppInboxClick(AppInboxAction action, AppInboxMessage message);
        void TrackAppInboxClickWithoutTrackingConsent(AppInboxAction action, AppInboxMessage message);
        Task<bool> MarkAppInboxAsRead(AppInboxMessage message);
        Task<IList<AppInboxMessage>> FetchAppInbox();
        Task<AppInboxMessage> FetchAppInboxItem(string messageId);
    }
}