using AMA.Common.Contracts;
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
    public partial class UserPage : ContentPage
    {
        private readonly UsersResponse _user;
        private ApiService _userService = new ApiService("users");
        public UserPage(Common.Contracts.UsersResponse user)
        {
            InitializeComponent();
            BindingContext = _user = user;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //if (_user.Id != ApiService.LoggedInUserId)
            //{
            //    logoutButton.IsVisible = false;
            //}

            //var followers = await _userService.Get<IList<UserFollowDto>>(null, $"followers/{_user.Id}");

            //if (followers.Any(x => x.Id == ApiService.LoggedInUserId))
            //    followButton.Text = "Unfollow";
        }
        private async void followButton_Clicked(object sender, EventArgs e)
        {
           
        }

        private async void messageButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new NavigationPage(new MessagePage(_user)));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void payButton_Clicked(object sender, EventArgs e)
        {

        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}