using AMA.MobileClient.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMA.MobileClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddQuestionPage : ContentPage
    {
        private readonly ApiService _questionsService = new ApiService("questions");
        private readonly AddQuestionViewModel _model = null;
        public AddQuestionPage()
        {
            InitializeComponent();
            BindingContext = _model = new AddQuestionViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _model.Init();
        }

        private async void Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            await _model.FilterSubCategories();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Title.Text) || string.IsNullOrWhiteSpace(Title.Text)
                    || string.IsNullOrEmpty(Body.Text) || string.IsNullOrWhiteSpace(Body.Text)
                    || _model.SelectedSubCategory.Id == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Check all fields", "OK");
                    return;
                }

                var subCategories = new List<int>();
                subCategories.Add(_model.SelectedSubCategory.Id);

                var request = new
                {
                    Title = Title.Text,
                    Body = Body.Text,
                    SubCategories = subCategories,
                    Tags = Tags.Text
                };

                await _questionsService.Post<object>(request, "add");
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}