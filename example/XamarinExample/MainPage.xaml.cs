using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinExample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            customerCookie.Text = "Customer cookie: \n" + DependencyService.Get<IActions>().GetCustomerCookie();
        }

        void Track_Clicked(System.Object sender, System.EventArgs e)
        {
            DependencyService.Get<IActions>().TrackEvent("custom_event");
        }

        void Track_Payment_Clicked(System.Object sender, System.EventArgs e)
        {
            DependencyService.Get<IActions>().TrackPayment();
        }

        void Track_Delivered_Clicked(System.Object sender, System.EventArgs e)
        {
            DependencyService.Get<IActions>().TrackDelivered();
        }
        void Track_Clicked_Clicked(System.Object sender, System.EventArgs e)
        {
            DependencyService.Get<IActions>().TrackClicked();
        }

        void Anonymize_Clicked(System.Object sender, System.EventArgs e)
        {
            DependencyService.Get<IActions>().Anonymize();
            customerCookie.Text = "Customer cookie: \n" + DependencyService.Get<IActions>().GetCustomerCookie();
        }

        void Flush_Clicked(System.Object sender, System.EventArgs e)
        {
            DependencyService.Get<IActions>().Flush(this);
        }
        void Fetch_Consents_Clicked(System.Object sender, System.EventArgs e)
        {
            DependencyService.Get<IActions>().FetchConsents(this);
        }
        void Fetch_Recomendations_Clicked(System.Object sender, System.EventArgs e)
        {
            DependencyService.Get<IActions>().FetchRecommendations(this, recommendationId.Text);
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
            DependencyService.Get<IActions>().IdentifyCustomer(registered, propertyName, propertyValue);
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

            DependencyService.Get<IActions>().SwitchProject(projectToken, authorization, baseUrl);
            Preferences.Set("projectToken", projectToken);
            Preferences.Set("authorization", authorization);
            Preferences.Set("baseURL", baseUrl);
            customerCookie.Text = "Customer cookie: \n" + DependencyService.Get<IActions>().GetCustomerCookie();
        }

        async void Show_Configuration_ClickedAsync(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ConfigInfoPage());
        }
    }
}
