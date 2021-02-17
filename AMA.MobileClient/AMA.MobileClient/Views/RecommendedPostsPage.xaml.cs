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
    public partial class RecommendedPostsPage : ContentPage
    {
        private readonly ApiService _usersService = new ApiService("users");
        private readonly RecommendedPostPageViewModel _model = null;
        public RecommendedPostsPage()
        {
            InitializeComponent();
            BindingContext = _model = new RecommendedPostPageViewModel();
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
    }
}