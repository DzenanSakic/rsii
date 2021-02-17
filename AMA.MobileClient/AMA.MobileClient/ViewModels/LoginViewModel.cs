using AMA.Common.Contracts;
using AMA.MobileClient.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace AMA.MobileClient.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private ApiService _authService = new ApiService("auth");

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
                        if(!string.IsNullOrEmpty(Username) || !string.IsNullOrWhiteSpace(Username)
                        || !string.IsNullOrEmpty(Password) || !string.IsNullOrWhiteSpace(Password))

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
