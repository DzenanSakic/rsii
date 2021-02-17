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
        public UserPage(UsersResponse user)
        {
            InitializeComponent();
            BindingContext = _user = user;
        }

        protected override async void OnAppearing()
        {
           base.OnAppearing();
        }
        private async void followButton_Clicked(object sender, EventArgs e)
        {
            var request = new
            {
                FollowingUserId = _user.Id,
                UserFolloingId = ApiService.UserId
            };

            _ = await _userService.Post<object>(request, "user/follow");
            Refresh(false);
        }

        private async void unfollowButton_Clicked(object sender, EventArgs e)
        {
            var request = new
            {
                UserFollowingId = ApiService.UserId,
                FollowedUserId = _user.Id
            };

            _ = await _userService.Delete<object>(request, "user/follow");
            Refresh(true);
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
            try
            {
                await Navigation.PushModalAsync(new NavigationPage(new PaymentPage(_user)));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}