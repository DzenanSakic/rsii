using AMA.Common.Contracts;
using AMA.MobileClient.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AMA.MobileClient.ViewModels
{
    public class RecommendedPostPageViewModel : BaseViewModel
    {
        private readonly ApiService _questionsService = new ApiService("questions");
        private readonly ApiService _answersService = new ApiService("answers");
        public ObservableCollection<QuestionAnswerModel> Questions { get; set; } = new ObservableCollection<QuestionAnswerModel>();

        public ICommand InitCommand { get; set; }
        public RecommendedPostPageViewModel()
        {
            InitCommand = new Command(async () => await Init());
        }

        public async Task Init()
        {
            try
            {
                Questions.Clear();

                IList<QuestionResponse> questions = null;

                questions = await _questionsService.Get<IList<QuestionResponse>>(null, "find/suggested");

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
                                IsAuthorOfAnswer = answer.UserId == ApiService.UserId ? true : false
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
            }
            catch 
            {
                throw;
            }
        }
    }
}
