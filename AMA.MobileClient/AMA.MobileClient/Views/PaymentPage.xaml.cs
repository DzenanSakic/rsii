using AMA.MobileClient.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMA.MobileClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : ContentPage
    {
        private readonly PaymentViewModel model;
        public PaymentPage(Common.Contracts.UsersResponse _user)
        {
            InitializeComponent();

            BindingContext = model = new PaymentViewModel(_user);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await model.Pay();

            if(result)
                await Navigation.PopModalAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}