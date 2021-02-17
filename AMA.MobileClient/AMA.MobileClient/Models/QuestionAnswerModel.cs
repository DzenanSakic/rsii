using AMA.Common.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AMA.MobileClient.Models
{
    public class QuestionAnswerModel 
    {
        public QuestionResponse Question { get; set; }
        public SubCategoryResponse SubCategoryResponse { get; set; }
        public ObservableCollection<AnswerResponse> Answers { get; set; }
        public bool HasCorrectAnswer { get; set; }
        public int AnswerCount { get; set; }
        public bool IsAuthorOfQuestion { get; internal set; }
        public bool IsFollowingCategory { get; internal set; }
        public bool CanFollowCategory { get; internal set; }
        public bool IsFollowingSubCategory { get; internal set; }
        public bool CanFollowSubCategory { get; internal set; }
    }
}
