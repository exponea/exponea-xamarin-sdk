#nullable enable
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exponea
{
    public class ExponeaSdk : IExponeaSdk
    {
        public string CustomerCookie => throw new NotImplementedException();

        public bool AutomaticSessionTracking { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FlushMode FlushMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan FlushPeriod { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public LogLevel LogLevel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsConfigured => throw new NotImplementedException();

        public void Anonymize()
        {
            throw new NotImplementedException();
        }

        public void Configure(Configuration config)
        {
            throw new NotImplementedException();
        }

        public void SwitchProject(Project project, IDictionary<EventType, IList<Project>> projectMapping)
        {
            throw new NotImplementedException();
        }

        public Task<string> FetchConsentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> FetchRecommendationsAsync(RecommendationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task FlushAsync()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> GetDefaultProperties()
        {
            throw new NotImplementedException();
        }

        public void IdentifyCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void SetDefaultProperties(IDictionary<string, object> properties)
        {
            throw new NotImplementedException();
        }

        public void Track(Event evt, double? timestamp)
        {
            throw new NotImplementedException();
        }

        public void Track(Delivery delivery)
        {
            throw new NotImplementedException();
        }

        public void Track(Click click)
        {
            throw new NotImplementedException();
        }

        public void Track(Payment payment, double? timestamp)
        {
            throw new NotImplementedException();
        }

        public void TrackPushToken(string token)
        {
            throw new NotImplementedException();
        }

        public void TrackSessionStart()
        {
            throw new NotImplementedException();
        }

        public void TrackSessionEnd()
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void CheckPushSetup()
        {
            throw new NotImplementedException();
        }

        public void SetInAppMessageDelegate(bool overrideDefaultBehavior, bool trackActions, Action<InAppMessage, string, string, bool> action)
        {
            throw new NotImplementedException();
        }

        public TokenTrackFrequency TokenTrackFrequency => throw new NotImplementedException();

        public void TrackInAppMessageClick(InAppMessage message, string buttonText, string buttonLink)
        {
            throw new NotImplementedException();
        }

        public void TrackInAppMessageClose(InAppMessage message)
        {
            throw new NotImplementedException();
        }

        public void SetAppInboxProvider(IDictionary<string, object> data)
        {
            throw new NotImplementedException();
        }

        public View GetAppInboxButton()
        {
            throw new NotImplementedException();
        }

        public void TrackWithoutTrackingConsent(Delivery delivery)
        {
            throw new NotImplementedException();
        }

        public void TrackWithoutTrackingConsent(Click click)
        {
            throw new NotImplementedException();
        }

        public void HandlePushNotificationOpened(Click click, string actionIdentifier)
        {
            throw new NotImplementedException();
        }

        public void HandlePushNotificationOpenedWithoutTrackingConsent(Click click, string actionIdentifier)
        {
            throw new NotImplementedException();
        }

        public void TrackCampaign(Uri url, double? timestamp = null)
        {
            throw new NotImplementedException();
        }

        public void HandlePushToken(string token)
        {
            throw new NotImplementedException();
        }

        public void HandleHmsPushToken(string token)
        {
            throw new NotImplementedException();
        }

        public void TrackInAppMessageClickWithoutTrackingConsent(InAppMessage message, string buttonText, string buttonLink)
        {
            throw new NotImplementedException();
        }

        public void TrackInAppMessageClose(InAppMessage message, bool? isUserInteraction = null)
        {
            throw new NotImplementedException();
        }

        public void TrackInAppMessageCloseWithoutTrackingConsent(InAppMessage message, bool? isUserInteraction = null)
        {
            throw new NotImplementedException();
        }

        public void SetPushNotificationsDelegate(Action<NotificationActionType, string, IDictionary<string, object>> action)
        {
            throw new NotImplementedException();
        }

        public void TrackAppInboxOpened(AppInboxMessage message)
        {
            throw new NotImplementedException();
        }

        public void TrackAppInboxOpenedWithoutTrackingConsent(AppInboxMessage message)
        {
            throw new NotImplementedException();
        }

        public void TrackAppInboxClick(AppInboxAction action, AppInboxMessage message)
        {
            throw new NotImplementedException();
        }

        public void TrackAppInboxClickWithoutTrackingConsent(AppInboxAction action, AppInboxMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkAppInboxAsRead(AppInboxMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<IList<AppInboxMessage>> FetchAppInbox()
        {
            throw new NotImplementedException();
        }

        public Task<AppInboxMessage> FetchAppInboxItem(string messageId)
        {
            throw new NotImplementedException();
        }
    }
}
