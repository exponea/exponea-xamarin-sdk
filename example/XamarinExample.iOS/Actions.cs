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
        
            Exponea.Instance.Anonymize(
                new NSDictionary(
                    "exponeaProject",
                    new NSDictionary(
                        "projectToken", projectToken,
                        "authorizationToken", authorization,
                        "baseUrl", baseURL
                   )
                )
            );
        }

        public void Configure(string projectToken, string authorization, string baseURL, bool automaticSessionTracking, string flushMode)
        {

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

            Exponea.Instance.Configure(configuration);
            Exponea.Instance.SetFlushMode(flushMode);
            Exponea.Instance.SetLogLevel("VERBOSE");
        }

        public void FetchConsents(Page page)
        {
            Action<NSString> success = delegate (NSString message) {
                page.DisplayAlert("Consents fetched", message, "OK");
            };

            Action<NSString> fail = delegate (NSString error) {
                page.DisplayAlert("Error", error, "OK");
            };

            Exponea.Instance.FetchConsents(success, fail);
        }

        public void FetchRecommendations(Page page, string recommendationID)
        {
            Action<NSString> successDelegate = delegate (NSString message) {
                page.DisplayAlert("Recommendations fetched", message, "OK");
            };

            Action<NSString> failDelegate = delegate (NSString error) {
                page.DisplayAlert("Error", error, "OK");
            };

            var options = new NSDictionary(
                "id", recommendationID,
                "fillWithRandom", true
            );

            Exponea.Instance.FetchRecommendations(options, successDelegate, failDelegate);
        }

        public void Flush(Page page)
        {
            Action<NSString> done = delegate (NSString message) {
                page.DisplayAlert("Flush finshed", message, "OK");
            };

            Exponea.Instance.FlushData(done);

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

            Exponea.Instance.IdentifyCustomer(customerIds, properties);
        }

        public void TrackClicked()
        {
            var properties = new NSDictionary(
                "action_type", "notification"
            );

            Exponea.Instance.TrackPushOpened(properties);
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
            Exponea.Instance.TrackEvent(eventName, properties, getTimestamp());
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
            Exponea.Instance.TrackPayment(
                new NSDictionary(
                      "value", "12.34",
                      "currency", "EUR",
                      "productId", "handbag",
                      "productTitle", "Awesome leather handbag"
                ),
                getTimestamp()
            );
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
