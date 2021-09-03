using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinExample
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            string authorizationPrefix = "";
            if (Device.RuntimePlatform == Device.Android)
            {
                authorizationPrefix = "Token ";
            }

            projectToken.Text = Preferences.Get("projectToken", "f02807dc-6b57-11e9-8cc8-0a580a203636");
            authorization.Text = Preferences.Get("authorization", authorizationPrefix + "ndtxrn9sd1g4zjpwye2b7fcnogl72kac6iqxyj7kae66vfahiimgqh3perwj7ssm");
            url.Text = Preferences.Get("baseURL", "https://api.exponea.com");
            flushMode.SelectedIndex = 0;
        }

        async void Configure_Clicked(System.Object sender, System.EventArgs e)
        {
            string mode = flushMode.Items[flushMode.SelectedIndex];
            DependencyService.Get<IActions>().Configure(
                    projectToken.Text,
                    authorization.Text,
                    url.Text,
                    automaticSessionTracking.IsToggled,
                    mode);
                Preferences.Set("projectToken", projectToken.Text);
                Preferences.Set("authorization", authorization.Text);
                Preferences.Set("baseURL", url.Text);

            
                await Navigation.PushAsync(new MainPage());
        }
    }
}
