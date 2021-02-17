using AMA.Common.Contracts;
using AMA.MobileClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMA.MobileClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage
    {
        private readonly ApiService _answersService = new ApiService("answers");
        private readonly ApiService _usersService = new ApiService("users");
        private readonly QuestionAnswerModel _model;
        public QuestionPage(QuestionAnswerModel question)
        {
            InitializeComponent();

            BindingContext = _model = question;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void markCorrectButton_Clicked(object sender, EventArgs e)
        {
            var answer = (AnswerResponse)((Button)sender).BindingContext;

            var responseAnswer = await _answersService.Post<AnswerResponse>(null, $"correct/{answer.ID}");

            var found = _model.Answers.FirstOrDefault(x => x.ID == answer.ID);
            int i = _model.Answers.IndexOf(found);
            found.Accepted = responseAnswer.Accepted;
            _model.Answers[i] = found;
        }

        private async void followSubCategory_Clicked(object sender, EventArgs e)
        {
            var request = new
            {
                UserId = ApiService.UserId,
                SubCategoryId = _model.SubCategoryResponse.Id
            };

            _ = await _usersService.Post<object>(request, "user/sub-category/follow");

            followSubCategory = NameScopeExtensions.FindByName<Button>(this, "followSubCategory");
            unfollowSubCategory = NameScopeExtensions.FindByName<Button>(this, "unfollowSubCategory");

            followSubCategory.IsVisible = false;
            unfollowSubCategory.IsVisible = true;
        }
        
        private async void unfollowSubCategory_Clicked(object sender, EventArgs e)
        {
            var request = new
            {
                UserId = ApiService.UserId,
                SubCategoryId = _model.SubCategoryResponse.Id
            };

            _ = await _usersService.Delete<object>(request, "user/sub-category/follow");

            followSubCategory = NameScopeExtensions.FindByName<Button>(this, "followSubCategory");
            unfollowSubCategory = NameScopeExtensions.FindByName<Button>(this, "unfollowSubCategory");

            followSubCategory.IsVisible = true;
            unfollowSubCategory.IsVisible = false;
        }

        private async void followCategory_Clicked(object sender, EventArgs e)
        {
            var request = new
            {
                UserId = ApiService.UserId,
                CategoryId = _model.SubCategoryResponse.Category.Id
            };

            _ = await _usersService.Post<object>(request, "user/category/follow");

            followCategory = NameScopeExtensions.FindByName<Button>(this, "followCategory");
            unfollowCategory = NameScopeExtensions.FindByName<Button>(this, "unfollowCategory");

            followCategory.IsVisible = false;
            unfollowCategory.IsVisible = true;
        }

        private async void unfollowCategory_Clicked(object sender, EventArgs e)
        {
            var request = new
            {
                UserId = ApiService.UserId,
                CategoryId = _model.SubCategoryResponse.Category.Id
            };

            _ = await _usersService.Delete<object>(request, "user/category/follow");

            followCategory = NameScopeExtensions.FindByName<Button>(this, "followCategory");
            unfollowCategory = NameScopeExtensions.FindByName<Button>(this, "unfollowCategory");

            followCategory.IsVisible = true;
            unfollowCategory.IsVisible = false;
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            var answer = (AnswerResponse)((Button)sender).BindingContext;

            _ = await _answersService.Delete<object>(null, $"delete/{answer.ID}");

            _model.Answers.Remove(answer);
        }

        private async void likeButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var answer = (AnswerResponse)((Button)sender).BindingContext;

            var request = new
            {
                UserId = ApiService.UserId,
                AnswerId = answer.ID,
                Rating = true
            };
            var aV = await _answersService.Post<List<AnswerVotingResponse>>(request, "votings/add");

            var negativeCount = aV.Count(x => x.Rating == false);
            var positiveCount = aV.Count(x => x.Rating == true);

            button.Text = $"{positiveCount} +";
        }

        private async void dislikeButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var answer = (AnswerResponse)((Button)sender).BindingContext;

            var request = new
            {
                UserId = ApiService.UserId,
                AnswerId = answer.ID,
                Rating = false
            };
            var aV = await _answersService.Post<List<AnswerVotingResponse>>(request, "votings/add");

            var negativeCount = aV.Count(x => x.Rating == false);
            var positiveCount = aV.Count(x => x.Rating == true);

            button.Text = $"- {negativeCount}";
        }

        private async void answerButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var bans = await _usersService.Get<List<BanResponse>>(null, $"user/{ApiService.UserId}/bans");
                var activeBan = bans.Where(x => x.DateTimeTo > DateTime.Now).FirstOrDefault();

                if(activeBan != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Your account is banned, ban will expire on {activeBan.DateTimeTo}", "OK");
                    return;
                }

                var answer = Answer.Text;

                if (string.IsNullOrEmpty(answer) || string.IsNullOrWhiteSpace(answer))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please insert a comment first", "OK");
                    return;
                }

                var apiResponse = await _answersService.Post<AnswerResponse>(new { QuestionId = _model.Question.ID, Text = answer },"add");

                if (apiResponse != null)
                {
                    apiResponse.IsAuthorOfAnswer = ApiService.UserId == apiResponse.UserId ? true : false;
                    apiResponse.IsAuthorOfQuestion = ApiService.UserId == _model.Question.UserId ? true : false;
                    _model.Answers.Add(apiResponse);
                }
                Answer.Text = "";
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


    }
}