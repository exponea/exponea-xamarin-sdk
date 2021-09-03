using System;
using Xamarin.Forms;
namespace XamarinExample
{
    public interface IActions
    {
        void Configure(string projectToken, string authorization, string baseURL, bool automaticSessionTracking, string flushMode);
        void TrackEvent(string eventName);
        void TrackDelivered();
        void TrackClicked();
        void TrackPayment();
        void Anonymize();
        void SwitchProject(string projectToken, string authorization, string baseURL);
        void Flush(Page page);
        string GetCustomerCookie();
        void FetchConsents(Page page);
        void FetchRecommendations(Page page, string recommendationID);
        void IdentifyCustomer(string registered, string propertyName, string propertyValue);
        bool IsAutoSessionTrackingOn();
        string GetFlushMode();
        int GetFlushPeriod();
        string GetLogLevel();
        string GetDefaultProperties();
    }
}
