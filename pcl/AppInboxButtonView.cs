using System;
using Xamarin.Forms;

namespace Exponea
{
    public class AppInboxButtonView : ContentView
    {

        private StackLayout StackLayout;

        public AppInboxButtonView()
        {
            StackLayout = new StackLayout();
            Content = StackLayout;
            StackLayout.Children.Add(new Exponea.ExponeaSdk().GetAppInboxButton());
        }

    }
}
