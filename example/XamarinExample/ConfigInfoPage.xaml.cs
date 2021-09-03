using Xamarin.Forms;

namespace XamarinExample
{
    public partial class ConfigInfoPage : ContentPage
    {
        public ConfigInfoPage()
        {
            InitializeComponent();
            automaticSessionTracking.Text = DependencyService.Get<IActions>().IsAutoSessionTrackingOn().ToString();
            flushMode.Text = DependencyService.Get<IActions>().GetFlushMode();
            flushPeriod.Text = DependencyService.Get<IActions>().GetFlushPeriod().ToString();
            logLevel.Text = DependencyService.Get<IActions>().GetLogLevel();
            defaultProperties.Text = DependencyService.Get<IActions>().GetDefaultProperties();
        }
    }
}
