using AMA.Common.Contracts;
using AMA.MobileClient.Models;
using AMA.MobileClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMA.MobileClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly ApiService _usersService = new ApiService("users");
        private readonly HomePageViewModel _model = null;
        public HomePage()
        {
            InitializeComponent();

            BindingContext = _model = new HomePageViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            await _model.Init();
        }

        private async void PostItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var followedCategories = await _usersService.Get<List<UserFollowCategoryResponse>>(null, "user/category/followings");
                var followedSubCategories = await _usersService.Get<List<UserFollowSubCategoryResponse>>(null, "user/sub-category/followings");

                var question = e.SelectedItem as QuestionAnswerModel;

                question.IsFollowingCategory = followedCategories.Any(x => x.CategoryId == question.SubCategoryResponse.Category.Id);
                question.CanFollowCategory = followedCategories.Where(x => x.CategoryId == question.SubCategoryResponse.Category.Id).Count() > 0 ? false : true;
                
                question.IsFollowingSubCategory = followedSubCategories.Any(x => x.SubCategoryId == question.SubCategoryResponse.Id);
                question.CanFollowSubCategory = followedSubCategories.Where(x => x.SubCategoryId == question.SubCategoryResponse.Id).Count() > 0 ? false : true;

                await Navigation.PushModalAsync(new NavigationPage(new QuestionPage(question)));
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
                var title = Title.Text;
                await _model.Filter(title);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var bans = await _usersService.Get<List<BanResponse>>(null, $"user/{ApiService.UserId}/bans");
                var activeBan = bans.Where(x => x.DateTimeTo > DateTime.Now).FirstOrDefault();

                if (activeBan != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Your account is banned, ban will expire on {activeBan.DateTimeTo}", "OK");
                    return;
                }

                var addQuestionPage = new NavigationPage(new AddQuestionPage());
                await Navigation.PushModalAsync(addQuestionPage);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            var question = (QuestionAnswerModel)((Button)sender).BindingContext;

            await _model.DeleteQuestion(question.Question.ID);

            _model.Questions.Remove(question);
        }

        private async void Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            await _model.FilterSubCategories();
        }
    }
}