using AMA.MobileClient.ViewModels;

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

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await model.Pay();

            await Navigation.PopModalAsync();
        }
    }
}