using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinExample
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            projectToken.Text = Preferences.Get("projectToken", "");
            authorization.Text = Preferences.Get("authorization", "");
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
