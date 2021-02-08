using AMA.MobileClient.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMA.MobileClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        private readonly RegisterViewModel _model = null;
        public RegistrationPage()
        {
            InitializeComponent();

            BindingContext = _model = new RegisterViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            await _model.Init();
        }

        private void Countries_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.FilterCities();
        }
    }
}