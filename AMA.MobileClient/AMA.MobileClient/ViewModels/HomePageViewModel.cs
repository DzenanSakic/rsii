using AMA.Common.Contracts;
using AMA.MobileClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AMA.MobileClient.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly ApiService _questionsService = new ApiService("questions");
        private readonly ApiService _answersService = new ApiService("answers");
        private readonly ApiService _categoriesService = new ApiService("categories");

        public ObservableCollection<QuestionAnswerModel> Questions { get; set; } = new ObservableCollection<QuestionAnswerModel>();
        public ICommand InitCommand { get; set; }

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

        public ObservableCollection<SubCategoryResponse> SubCategories { get; set; } = new ObservableCollection<SubCategoryResponse>();

        public HomePageViewModel()
        {
            InitCommand = new Command(async () => await Init());
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

        internal async Task DeleteQuestion(int iD)
        {
            _ = await _questionsService.Delete<object>(null, $"delete/{iD}");
        }

        public async Task Init()
        {
            try
            {
                Questions.Clear();

                IList<QuestionResponse> questions = null;

                questions = await _questionsService.Get<IList<QuestionResponse>>(null, "find");
                 
                foreach (var item in questions)
                {
                    var questionCategory = (await _questionsService.Get<List<QuestionSubCategoryResponse>>(null, $"category/{item.ID}")).FirstOrDefault();
                    var questionAnswers = (await _answersService.Get<List<AnswerResponse>>(null, $"all/{item.ID}")).ToList().OrderByDescending(x => x.Accepted);
                    
                    var answersHelp = new ObservableCollection<AnswerResponse>();
                    foreach (var answer in questionAnswers)
                    {
                        var answerVotings = (await _answersService.Get<List<AnswerVotingResponse>>(null, $"votings/{answer.ID}")).ToList();
                        var positiveVotingsHelp = new int();
                        var negativeVotingsHelp = new int();

                        foreach (var av in answerVotings)
                        {
                            if (av.Rating)
                                positiveVotingsHelp++;
                            else
                                negativeVotingsHelp++;
                        }

                        answersHelp.Add(
                            new AnswerResponse
                            {
                                Accepted = answer.Accepted,
                                ID = answer.ID,
                                QuestionId = answer.QuestionId,
                                Text = answer.Text,
                                TimeAnswered = answer.TimeAnswered,
                                User = answer.User,
                                UserId = answer.UserId,
                                PositiveVotings = positiveVotingsHelp,
                                NegativeVotings = negativeVotingsHelp,
                                IsAuthorOfQuestion = item.UserId == ApiService.UserId ? true : false,
                                IsAuthorOfAnswer = answer.UserId== ApiService.UserId ? true : false
                            });
                    }

                    Questions.Add(new QuestionAnswerModel
                    {
                        SubCategoryResponse = new SubCategoryResponse { Category = questionCategory.SubCategory.Category, Id = questionCategory.SubCategoryId, Name = questionCategory.SubCategory.Name },
                        Question = item,
                        Answers = answersHelp,
                        AnswerCount = questionAnswers.Count(),
                        HasCorrectAnswer = questionAnswers.Any(x => x.Accepted),
                        IsAuthorOfQuestion = item.UserId == ApiService.UserId ? true : false
                    });
                }

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
        public async Task Filter(string Title)
        {
            try
            {
                int subCategoryId;
                int categoryId;

                if (SelectedSubCategory == null)
                    subCategoryId = 0;
                else
                    subCategoryId = SelectedSubCategory.Id;

                if (SelectedCategory == null)
                    categoryId = 0;
                else
                    categoryId = SelectedCategory.Id;


                var postSearch = await _questionsService.Get<IList<QuestionResponse>>(new 
                {
                    Title = Title,
                    SubCategoryId = subCategoryId,
                    CategoryId = categoryId
                }, "find");

                Questions.Clear();

                foreach (var item in postSearch)
                {
                    var questionCategory = (await _questionsService.Get<List<QuestionSubCategoryResponse>>(null, $"category/{item.ID}")).FirstOrDefault();
                    var questionAnswers = (await _answersService.Get<List<AnswerResponse>>(null, $"all/{item.ID}")).ToList().OrderByDescending(x => x.Accepted);

                    var answersHelp = new ObservableCollection<AnswerResponse>();
                    foreach (var answer in questionAnswers)
                    {
                        answersHelp.Add(answer);
                    }

                    Questions.Add(new QuestionAnswerModel
                    {
                        SubCategoryResponse = new SubCategoryResponse { Category = questionCategory.SubCategory.Category, Id = questionCategory.SubCategoryId, Name = questionCategory.SubCategory.Name },
                        Question = item,
                        Answers = answersHelp,
                        AnswerCount = questionAnswers.Count(),
                        HasCorrectAnswer = questionAnswers.Any(x => x.Accepted)
                    });
                }
            }
            catch (Exception)
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
    }
}
