using Exponea;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinExample
{
    public partial class LoginPage : ContentPage
    {
        private readonly IExponeaSdk _exponea = DependencyService.Get<IExponeaSdk>();

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
            _exponea.Configure(new Project(projectToken.Text, authorization.Text, url.Text));
            _exponea.AutomaticSessionTracking = automaticSessionTracking.IsToggled;
            _exponea.FlushMode = (FlushMode)flushMode.SelectedItem;

            await Navigation.PushAsync(new MainPage());
        }
    }
}
