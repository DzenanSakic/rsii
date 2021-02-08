using AMA.MobileClient.Models;
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
    public partial class MessagesPage : ContentPage
    {
        private MessagesViewModel _model;
        public MessagesPage()
        {
            InitializeComponent();

            BindingContext = _model = new MessagesViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await _model.Init();
        }

        private async void UserMessagesItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var userMessages = e.SelectedItem as UserMessagesModel;
                await Navigation.PushModalAsync(new NavigationPage(new ViewMessagesPage(userMessages)));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}