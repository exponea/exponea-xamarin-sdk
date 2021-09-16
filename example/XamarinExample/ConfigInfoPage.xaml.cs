using Exponea;
using System.Text.Json;
using Xamarin.Forms;

namespace XamarinExample
{
    public partial class ConfigInfoPage : ContentPage
    {
        private readonly IExponeaSdk _exponea;

        public ConfigInfoPage()
        {
            InitializeComponent();

            BindingContext = _exponea = DependencyService.Get<IExponeaSdk>();
            defaultProperties.Text = _exponea.GetDefaultProperties() != null ? JsonSerializer.Serialize(_exponea.GetDefaultProperties()) : "";
        }

    }     
    
}
