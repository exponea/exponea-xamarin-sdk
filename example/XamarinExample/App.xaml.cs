using System;
using System.Collections.Generic;
using Exponea;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinExample
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
      
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
