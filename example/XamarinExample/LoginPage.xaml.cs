using System.Collections.Generic;
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

            if (_exponea.IsConfigured)
            {
                GoToNextPage();
            }
        }

        void Configure_Clicked(System.Object sender, System.EventArgs e)
        {
            var config = new Configuration(projectToken.Text, authorization.Text, url.Text);
            config.AutomaticSessionTracking = automaticSessionTracking.IsToggled;

            var projectMapping = new Dictionary<EventType, IList<Project>>();
            projectMapping.Add(EventType.TrackEvent, new List<Project>()
                    {
                        new Project(
                            projectToken: "2087f0ae-6b58-11e9-a353-0a580a2009c0",
                            authorization: "vqgp8d3789w9dxr1srtal7nmi6sqn9fwakhptn1kbp40lb484pbh4v5a750lf817",
                            baseUrl: "https://api.exponea.com"
                            )
                    });

            config.ProjectRouteMap = projectMapping;
            config.DefaultProperties = new Dictionary<string, object>()
            {
                { "thisIsADefaultStringProperty", "This is a default string value" },
                { "thisIsADefaultIntProperty", 1},
                { "thisIsADefaultDoubleProperty", 12.53623}

            };

            _exponea.Configure(config);
            _exponea.FlushMode = (FlushMode)flushMode.SelectedItem;

            Preferences.Set("projectToken", projectToken.Text);
            Preferences.Set("authorization", authorization.Text);
            Preferences.Set("baseURL", url.Text);

            GoToNextPage();
        }

        async void GoToNextPage()
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}
