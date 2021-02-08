using AMA.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AMA.MobileClient.ViewModels
{
    public class AddQuestionViewModel : BaseViewModel
    {
        private readonly ApiService _categoriesService = new ApiService("categories");
        public ICommand InitCommand { get; set; }

        public AddQuestionViewModel()
        {
            InitCommand = new Command(async () => await Init());
        }

        public async Task Init()
        {
            try
            {
                Categories.Clear();
                SubCategories.Clear();
                var categories = await _categoriesService.Get<List<CategoryResponse>>(null, "all");
                categories.Insert(0, new CategoryResponse { Id = 0, Name = "" });
                foreach (var item in categories)
                {
                    Categories.Add(item);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task FilterSubCategories()
        {
            try
            {
                var subCategories = (await _categoriesService.Get<List<SubCategoryResponse>>(null, $"sub-categories/{SelectedCategory.Id}"));
                subCategories.Insert(0, new SubCategoryResponse { Id = 0, Name = "" });
                SubCategories.Clear();

                foreach (var item in subCategories)
                {
                    SubCategories.Add(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private CategoryResponse _category;
        public CategoryResponse SelectedCategory
        {
            get
            {
                return _category;
            }
            set
            {
                SetProperty(ref _category, value);
            }
        }

        private SubCategoryResponse _subCategory;
        public SubCategoryResponse SelectedSubCategory
        {
            get
            {
                return _subCategory;
            }
            set
            {
                SetProperty(ref _subCategory, value);
            }
        }

        public ObservableCollection<SubCategoryResponse> SubCategories { get; set; } = new ObservableCollection<SubCategoryResponse>();

        private ObservableCollection<CategoryResponse> _categories = new ObservableCollection<CategoryResponse>();
        public ObservableCollection<CategoryResponse> Categories
        {
            get { return _categories; }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    SetProperty(ref _categories, value);
                }
            }
        }
    }
}
