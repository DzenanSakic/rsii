using AMA.Common.Contracts;
using AMA.MobileClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMA.MobileClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {
        private UsersViewModel _model;

        public UsersPage()
        {
            InitializeComponent();

            BindingContext = _model = new UsersViewModel();
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await _model.Init();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void UsersItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var user = e.SelectedItem as UsersResponse;
                await Navigation.PushModalAsync(new NavigationPage(new UserPage(user)));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        private async void FilterButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var username = UserName.Text;
                await _model.Filter(username);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
