using System;
using Xamarin.Forms;
using ExponeaSdk;
using Foundation;
using UIKit;

[assembly: Dependency(typeof(XamarinExample.iOS.Actions))]
namespace XamarinExample.iOS
{
    public class Actions : IActions
    {
        public void Anonymize()
        {
            Exponea.Instance.Anonymize();
        }

        public void SwitchProject(string projectToken, string authorization, string baseURL)
        {
            var exponeaProjectDictionary = new NSDictionary(
               "exponeaProject", new NSDictionary(
                    "projectToken", projectToken,
                    "authorizationToken", authorization,
                    "baseUrl", baseURL
                   )
           );
            Exponea.Instance.AnonymizeWithExponeaProjectDictionary(exponeaProjectDictionary, null);
        }

        public void Configure(string projectToken, string authorization, string baseURL, bool automaticSessionTracking, string flushMode)
        {
            Console.Out.WriteLine("Going to configure");
            var configuration = new NSDictionary(
                "projectToken", projectToken,
                "authorizationToken", authorization,
                "baseUrl", baseURL,
                "defaultProperties", new NSDictionary(
                    "thisIsADefaultStringProperty", "This is a default string value",
                    "thisIsADefaultIntProperty", 1
                    ),
                "automaticSessionTracking", automaticSessionTracking
                
            );

            Exponea.Instance.ConfigureWithConfiguration(configuration);
            Exponea.Instance.SetFlushModeWithFlushMode(flushMode);
        }

        public void FetchConsents(Page page)
        {
            Action<NSString> success = delegate (NSString message) {
                page.DisplayAlert("Consents fetched", message, "OK");
            };

            Action<NSString> fail = delegate (NSString error) {
                page.DisplayAlert("Error", error, "OK");
            };

            Exponea.Instance.FetchConsentsWithSuccess(success: success, fail);
        }

        public void FetchRecommendations(Page page, string recommendationID)
        {
            Action<NSString> success = delegate (NSString message) {
                page.DisplayAlert("Recommendations fetched", message, "OK");
            };

            Action<NSString> fail = delegate (NSString error) {
                page.DisplayAlert("Error", error, "OK");
            };

            var options = new NSDictionary(
                "id", recommendationID,
                "fillWithRandom", true
            );

            Exponea.Instance.FetchRecommendationsWithOptionsDictionary(options, success, fail);
        }

        public void Flush(Page page)
        {
            Action<NSString> done = delegate (NSString message) {
                page.DisplayAlert("Flush finshed", message, "OK");
            };

            Exponea.Instance.FlushDataWithDone(done);
        }

        public string GetCustomerCookie()
        {
            return Exponea.Instance.CustomerCookie;
        }

        public void IdentifyCustomer(string registered, string propertyName, string propertyValue)
        {
            var customerIds = new NSDictionary(
                "registered", registered
            );

            var properties = new NSDictionary(
                propertyName, propertyValue
            );

            Exponea.Instance.IdentifyCustomerWithCustomerIds(customerIds, properties);
        }

        public void TrackClicked()
        {
            var properties = new NSDictionary(
                "action_type", "notification"
            );

            Exponea.Instance.TrackPushOpenedWithUserInfo(properties);
        }

        public void TrackDelivered()
        {
            ShowToast("Not available for iOS", 2);
        }

        public void TrackEvent(string eventName)
        {
            var properties = new NSDictionary(
                "number", 6,
                "string", "hello",
                "double", 4534.234234
            );
            Exponea.Instance.TrackEventWithEventType(eventName, properties, getTimestamp());
        }

        private double getTimestamp()
        {
            return Convert.ToDouble(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000);
        }

        public bool IsAutoSessionTrackingOn()
        {
            return Exponea.Instance.IsAutoSessionTrackingEnabled;
        }

        public string GetFlushMode()
        {
            return Exponea.Instance.FlushMode;
        }

        public string GetDefaultProperties()
        {
            return Exponea.Instance.DefaultProperties;
        }

        public int GetFlushPeriod()
        {
            return (int)Exponea.Instance.FlushPeriod;
        }

        public string GetLogLevel()
        {
            return Exponea.Instance.LogLevel;
        }
        public void TrackPayment()
        {
            var properties = new NSDictionary(
              "value", "99",
              "custom_info", "sample payment"
           );
            Exponea.Instance.TrackPaymentWithProperties(properties, getTimestamp());
        }

        private void ShowToast(string message, double seconds)
        {
            UIAlertController alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);

            NSTimer alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                alert.DismissViewController(true, null);
            });

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }
    }
}
