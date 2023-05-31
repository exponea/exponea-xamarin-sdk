using System;
using Exponea;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Collections.Generic;

namespace XamarinExample
{
    public partial class MainPage : ContentPage
    {
        private readonly IExponeaSdk _exponea;

        public MainPage()
        {
            InitializeComponent();

            _exponea = DependencyService.Get<IExponeaSdk>();
            customerCookie.Text = "Customer cookie: \n" + _exponea.CustomerCookie;
            SessionStartButton.IsVisible = !_exponea.AutomaticSessionTracking;
            SessionEndButton.IsVisible = !_exponea.AutomaticSessionTracking;
            RegisterForPush.IsVisible = Device.RuntimePlatform == Device.iOS;

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("title", "ČAU Z XAMARINU");
            //_exponea.SetAppInboxProvider(dic);

            View button = _exponea.GetAppInboxButton();
            AppInboxButtonHere.Children.Add(button);

            _exponea.SetInAppMessageDelegate(overrideDefaultBehavior: false, trackActions: true, action: async delegate (InAppMessage message, string buttonText, string buttonUrl, bool interaction)
            {
                await DisplayAlert(
                    "In-App message action delegate called",
                    String.Format(
                        "MessageId: {0} \n Button text: {1} \n " +
                        "Button url: {2} \n Interaction: {3}",
                        message.Id, buttonText, buttonUrl, interaction
                        ),
                    "OK");
            });
        }

        void Track_Clicked(System.Object sender, System.EventArgs e)
        {
            _exponea.Track(new Event("custom_event") { ["thisIsAStringProperty"] = "thisIsAStringValue" });
        }

        void Track_Payment_Clicked(System.Object sender, System.EventArgs e)
        {
            _exponea.Track(new Payment(12.34, "EUR", "Virtual", "handbag", "Awesome leather handbag"));
        }

        void Track_Delivered_Clicked(System.Object sender, System.EventArgs e)
        {
            _exponea.Track(new Delivery { ["campaign_id"] = "id" });
        }
        void Track_Clicked_Clicked(System.Object sender, System.EventArgs e)
        {
            _exponea.Track(new Click("click", "action") { ["campaign_id"] = "id" });
        }

        void Anonymize_Clicked(System.Object sender, System.EventArgs e)
        {
            _exponea.Anonymize();
            customerCookie.Text = "Customer cookie: \n" + _exponea.CustomerCookie;
        }

        async void Flush_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                await _exponea.FlushAsync();
                await DisplayAlert("Flush finished", "Flush finished", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Flush failed", ex.Message, "OK");
            }
        }
        async void Fetch_Consents_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var res = await _exponea.FetchConsentsAsync();
                await DisplayAlert("Consents fetched", res, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Consent fetch failed", ex is FetchException fe ? fe.JsonBody : ex.Message, "OK");
            }
        }

        async void Fetch_Recomendations_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var res = await _exponea.FetchRecommendationsAsync(
                    new RecommendationsRequest(
                        id: recommendationId.Text, fillWithRandom: true)
                    );
                await DisplayAlert("Recommendations fetched", res, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Recommendations fetch failed", ex is FetchException fe ? fe.JsonBody : ex.Message, "OK");
            }
        }

        async void Identify_Customer_ClickedAsync(System.Object sender, System.EventArgs e)
        {
            string registered = await DisplayPromptAsync("Identify customer", "Registered");
            string propertyName = await DisplayPromptAsync("Identify customer", "Property name");
            string propertyValue = await DisplayPromptAsync("Identify customer", "Property value");
            // Preparing the data.

            if (string.IsNullOrEmpty(registered) || string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(propertyValue))
            {
                await DisplayAlert("Error", "One or more fields were left empty, skipping identifying the customer.", "OK");
                return;
            }
            Customer Customer = new Customer(registered) { [propertyName] = propertyValue };
            CustomerTokenStorage.INSTANCE.Configure(
                customerIds: Customer.ExternalIds
            );
            _exponea.IdentifyCustomer(Customer);
        }

        async void Switch_Project_ClickedAsync(System.Object sender, System.EventArgs e)
        {
            string projectToken = await DisplayPromptAsync("Switch project", "Project token");
            string authorization = await DisplayPromptAsync("Switch project", "Authorization");
            string baseUrl = await DisplayPromptAsync("Switch project", "Base URL");

            if (string.IsNullOrEmpty(projectToken) || string.IsNullOrEmpty(authorization) || string.IsNullOrEmpty(baseUrl))
            {
                await DisplayAlert("Error", "One or more fields were left empty, skipping switching the project.", "OK");
                return;
            }

            _exponea.SwitchProject(new Project(projectToken, authorization, baseUrl));
            customerCookie.Text = "Customer cookie: \n" + _exponea.CustomerCookie;
            Preferences.Set("projectToken", projectToken);
            Preferences.Set("authorization", authorization);
            Preferences.Set("baseURL", baseUrl);
        }

        async void Show_Configuration_ClickedAsync(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ConfigInfoPage());
        }

        void Track_Session_Start_Clicked(System.Object sender, System.EventArgs e)
        {
            _exponea.TrackSessionStart();
        }

        void Track_Session_End_Clicked(System.Object sender, System.EventArgs e)
        {
            _exponea.TrackSessionEnd();
        }

        void Register_For_Push_Clicked(System.Object sender, System.EventArgs e)
        {
            Customer Customer = new Customer("XamarinUser." + Guid.NewGuid()) { ["apple_push_notification_bundle_id"] = "com.exponea.xamarin" };
            CustomerTokenStorage.INSTANCE.Configure(
                customerIds: Customer.ExternalIds
            );
            _exponea.IdentifyCustomer(Customer);
            DependencyService.Get<IPushRegistrationHandler>().RegisterForRemoteNotifications();
        }

        void AppInboxButton_Clicked(System.Object sender, System.EventArgs e)
        {
            //Navigation.PushModalAsync((Page)_exponea.GetAppInboxListViewController());
        }

        async void Fetch_AppInbox_ClickedAsync(System.Object sender, System.EventArgs e)
        {
            try
            {
                var res = await _exponea.FetchAppInbox();
                await DisplayAlert("Recommendations fetched", "Got messages: " + res.Count, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Recommendations fetch failed", ex is FetchException fe ? fe.JsonBody : ex.Message, "OK");
            }
        }

        async void Fetch_AppInboxItem_ClickedAsync(System.Object sender, System.EventArgs e)
        {
            try
            {
                var all = await _exponea.FetchAppInbox();
                var res = await _exponea.FetchAppInboxItem(all[0].Id);
                await DisplayAlert("Recommendations fetched", "Got message", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Recommendations fetch failed", ex is FetchException fe ? fe.JsonBody : ex.Message, "OK");
            }
        }
    }
}
