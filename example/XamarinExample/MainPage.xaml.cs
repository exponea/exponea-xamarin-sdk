using System;
using Exponea;
using Xamarin.Essentials;
using Xamarin.Forms;

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
        }

        void Track_Clicked(System.Object sender, System.EventArgs e)
        {
            _exponea.Track(new Event("custom_event"));
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
                var res = await _exponea.FetchRecommendationsAsync(new RecommendationsRequest(recommendationId.Text));
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

            _exponea.IdentifyCustomer(new Customer(registered) { [propertyName] = propertyValue });
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
    }
}
