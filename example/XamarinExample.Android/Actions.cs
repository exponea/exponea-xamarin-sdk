﻿using System;
using ExponeaSdk;
using ExponeaSdk.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;
using Android.Runtime;

[assembly: Dependency(typeof(XamarinExample.Droid.Actions))]
namespace XamarinExample.Droid
{
    public class Actions : Java.Lang.Object, IActions
    {
        public void Anonymize()
        {
            Exponea.Instance.Anonymize();
        }

        public void SwitchProject(string projectToken, string authorization, string baseURL)
        {
            Exponea.Instance.Anonymize(new ExponeaProject(baseURL, projectToken, authorization));
        }

        public void Configure(string projectToken, string authorization, string baseURL, bool automaticSessionTracking, string flushMode)
        {
            ExponeaConfiguration config = new ExponeaConfiguration();
            config.ProjectToken = projectToken;
            config.Authorization = authorization;
            config.BaseURL = baseURL;
            config.DefaultProperties["thisIsADefaultStringProperty"] = "This is a default string value";
            config.DefaultProperties["thisIsADefaultIntProperty"] = 1;
            config.AutomaticSessionTracking = automaticSessionTracking;

            Exponea.Instance.Init(Platform.CurrentActivity, config);
            Exponea.Instance.FlushMode = FlushMode.ValueOf(flushMode);
            Exponea.Instance.LoggerLevel = Com.Exponea.Sdk.Util.Logger.Level.Debug;

         
        }

        public void Flush(Page page)
        {
            Exponea.Instance.FlushData(new ExponeaCallback<Kotlin.Result>((response) =>
            {
                Platform.CurrentActivity.RunOnUiThread(async () =>
                {
                    await page.DisplayAlert("Flush finshed", response.ToString(), "OK");
                });
            }));
        }

        public string GetCustomerCookie()
        {
            return Exponea.Instance.CustomerCookie;
        }

        public void TrackDelivered()
        {
            Dictionary<string, Java.Lang.Object> data = new Dictionary<string, Java.Lang.Object>
            {
                { "campaign_id", "id"}
            };
            Exponea.Instance.TrackDeliveredPush(new NotificationData(data, new CampaignData()), getTimestamp());
        }

        public void TrackClicked()
        {
            Dictionary<string, Java.Lang.Object> data = new Dictionary<string, Java.Lang.Object>
            {
                { "campaign_id", "id"}
            };
            Exponea.Instance.TrackClickedPush(new NotificationData(data, new CampaignData()), null, getTimestamp());
        }

        public void TrackPayment()
        {
            PurchasedItem item = new PurchasedItem(
                 12.34,
                 "EUR",
                 "Virtual",
                 "handbag",
                 "Awesome leather handbag"
            );

            //timestamp - event timestamp in seconds
            Exponea.Instance.TrackPaymentEvent(getTimestamp(), item);
        }

        public void TrackEvent(string eventName)
        {
          
            var properties = new PropertiesList(
                new Dictionary<string, Java.Lang.Object>
                    {
                        {"name", "John"},
                        {"age", 30},
                        {"height", 175.32}
                    });

            Exponea.Instance.TrackEvent(properties, getTimestamp(), eventName);
        }

        public void FetchConsents(Page page)
        {
            Exponea.Instance.GetConsents(
                new ExponeaCallback<Result>((response) =>
                {

                    Platform.CurrentActivity.RunOnUiThread(async () =>
                    {
                        await page.DisplayAlert("Consents fetched", response.ToString(), "OK");
                    });

                }), new ExponeaCallback<Result>((error) =>
                {
                    Platform.CurrentActivity.RunOnUiThread(async () =>
                    {
                        await page.DisplayAlert("Error", ((FetchError)error.Results).JsonBody, "OK");
                    });

                }));
        }

        private double getTimestamp()
        {
            return Convert.ToDouble(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000);
        }

        public void FetchRecommendations(Page page, string recommendationID)
        {
            if (recommendationID != null)
            {
                CustomerRecommendationOptions options = new CustomerRecommendationOptions(
                       id: recommendationID,
                       fillWithRandom: true,
                       size: 10);
                Exponea.Instance.FetchRecommendation(
                    options,

                new ExponeaCallback<Result>((response) =>
                {

                    Platform.CurrentActivity.RunOnUiThread(async () =>
                    {
                        await page.DisplayAlert("Recommendations fetched", response.ToString(), "OK");
                    });

                }), new ExponeaCallback<Result>((error) =>
                {
                    Platform.CurrentActivity.RunOnUiThread(async () =>
                    {
                        await page.DisplayAlert("Error", ((FetchError)error.Results).JsonBody, "OK");
                    });

                }));
            }
        }

        public void IdentifyCustomer(string registered, string propertyName, string propertyValue)
        {

            CustomerIds customerIds = new CustomerIds().WithId("registered", registered);
            Dictionary<string, Java.Lang.Object> properties = new Dictionary<string, Java.Lang.Object>
            {
                { propertyName, propertyValue}
            };
           
            Exponea.Instance.IdentifyCustomer(customerIds, new PropertiesList(properties));
        }

        public bool IsAutoSessionTrackingOn()
        {
            return Exponea.Instance.AutomaticSessionTracking;
        }

        public string GetFlushMode()
        {
            return Exponea.Instance.FlushMode.Name();
        }

        public int GetFlushPeriod()
        {
            return (int)Exponea.Instance.FlushPeriod.Amount;
        }

        public string GetLogLevel()
        {
            return Exponea.Instance.LoggerLevel.Name();
        }

        public string GetDefaultProperties()
        {
            string properties = "";

            foreach (KeyValuePair<string, Java.Lang.Object> entry in Exponea.Instance.DefaultProperties)
            {
                properties += entry.Key + " : " + entry.Value.ToString() + Environment.NewLine;
            }
            return properties;
        }
    }
}
