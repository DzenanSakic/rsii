using AMA.Common.Contracts;
using AMA.MobileClient.Models;
using System.Collections.Generic;

namespace AMA.MobileClient.ViewModels
{
    public class QuestionPageViewModel : BaseViewModel
    {
        public QuestionAnswerModel Item { get; set; }
        public QuestionPageViewModel(QuestionAnswerModel model = null)
        {
            Title = model?.Question.Title;

            if (model != null)
            {
                Item = new QuestionAnswerModel
                {
                    AnswerCount = model.AnswerCount,
                    Question = model.Question,
                    Answers = model.Answers,
                    HasCorrectAnswer = model.HasCorrectAnswer,
                    SubCategoryResponse = model.SubCategoryResponse
                };
            }
        }
    }
}
