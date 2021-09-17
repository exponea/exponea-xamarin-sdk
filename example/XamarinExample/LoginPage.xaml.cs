using System;
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
            
            config.DefaultProperties = new Dictionary<string, object>()
            {
                { "thisIsADefaultStringProperty", "This is a default string value" },
                { "thisIsADefaultIntProperty", 1},
                { "thisIsADefaultDoubleProperty", 12.53623}

            };
            config.TokenTrackFrequency = TokenTrackFrequency.Daily;
        
            _exponea.Configure(config);
            _exponea.FlushMode = (FlushMode)flushMode.SelectedItem;
            _exponea.LogLevel = LogLevel.Debug;

            if (_exponea.FlushMode == FlushMode.Period && period.Text.Trim() != "" )
            {
                int minutes;
                bool isParsable = Int32.TryParse(period.Text, out minutes);

                if (isParsable)
                    _exponea.FlushPeriod = new System.TimeSpan(0, minutes: minutes, 0);
                else
                    Console.WriteLine("Period could not be parsed.");
               
            }
            _exponea.SetDefaultProperties(new Dictionary<string, object>()
            {
                { "thisIsADefaultStringProperty", "This is a default string value" },
                { "thisIsADefaultIntProperty", 1},
                { "thisIsADefaultDoubleProperty", 12.53623}

            });

            Preferences.Set("projectToken", projectToken.Text);
            Preferences.Set("authorization", authorization.Text);
            Preferences.Set("baseURL", url.Text);

            GoToNextPage();
        }

        async void GoToNextPage()
        {
            await Navigation.PushAsync(new MainPage());
        }

        void flushMode_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if ((FlushMode)flushMode.SelectedItem == FlushMode.Period)
            {
                period.IsVisible = true;
            } else
            {
                period.IsVisible = false;
            }
        }
    }
}
