using AMA.Common.Contracts;
using AMA.MobileClient.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AMA.MobileClient.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private ApiService _authService = new ApiService("auth");
        private ApiService _usersService = new ApiService("users");

        public LoginViewModel()
        {
            LoginCommand = new Command(async () =>
            {
                try
                {
                    IsBusy = true;
                    LoginResponse authentication = null;

                    try
                    {
                        authentication = await _authService.Post<LoginResponse>(new
                        {
                            username = Username,
                            password = Password
                        }, "login");
                    }
                    catch (Exception)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Login failed, please check your username and password", "OK");
                        return;
                    }

                    if (authentication != null)
                    {
                        ApiService.Token = authentication.AccessToken;
                        ApiService.Permission = authentication.Role;
                        ApiService.UserId = authentication.Id;

                        //SignalR.HubConnection = new HubConnectionBuilder()
                        //    .WithUrl($"{ApiService.Url}/notifications", options =>
                        //    {
                        //        options.AccessTokenProvider = () => Task.FromResult(ApiService.Token);
                        //    })
                        //    .Build();

                        //SignalR.HubConnection.On<string, string>("ReceiveNotification", (userFollowers, creator) =>
                        //{
                        //    var followers = JsonConvert.DeserializeObject<IList<int>>(userFollowers);

                        //    if (followers.Any(x => x == ApiService.LoggedInUserId))
                        //    {
                        //        Application.Current.MainPage.DisplayAlert("Alert", $"User {creator} added a new post", "OK");
                        //    }

                        //});

                        Application.Current.MainPage = new MainPage();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Login failed, please check your username and password", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            });


            RegistrationCommand = new Command(() =>
            {
                Application.Current.MainPage = new RegistrationPage();
            });
        }


        private string _password;
        private string _username;
        private string _image;

        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
            }
        }
        public string Image
        {
            get => _image;
            set
            {
                SetProperty(ref _image, value);
            }
        }
        public ICommand LoginCommand { get; set; }
        public ICommand RegistrationCommand { get; set; }
    }
}
