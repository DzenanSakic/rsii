using AMA.Common.Contracts;
using AMA.MobileClient.Models;
using AMA.MobileClient.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMA.MobileClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : ContentPage
    {
        private readonly MessageViewModel _model;
        private UserMessagesModel userMessageModel;

        public MessagePage(UsersResponse user)
        {
            InitializeComponent();

            BindingContext = _model = new MessageViewModel(user);
        }

        public MessagePage(UserMessagesModel model)
        {
            InitializeComponent();

            userMessageModel = model;

            BindingContext = _model = new MessageViewModel(model.User);
        }

        private async void SendButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var request = new
                {
                    ToUserId = _model.User.Id,
                    Title = _model.Title,
                    Body = _model.Body
                };

                await _model.SendMessage(request);
                await Application.Current.MainPage.DisplayAlert("Info", "Message sent", "Ok");

                if (userMessageModel != null)
                    userMessageModel.Messages.Add(new MessagesResponse
                    {
                        Body = request.Body,
                        Title = request.Title,
                        ToUserId = request.ToUserId,
                        FromUserId = ApiService.UserId
                    });

                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}