using AMA.Common.Contracts;
using AMA.Models.DTOS;
using AMA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IRepositoryAnswer _repositoryAnswer;
        private readonly IRepositoryQuestion _repositoryQuestion;
        private readonly IRepositoryQuestionSubCategory _repositoryQuestionSubCategory;
        private readonly IRepositoryAnswerVoting _repositoryAnswerVoting;
        public ReportService(IRepositoryUser repositoryUser, 
            IRepositoryAnswer repositoryAnswer, 
            IRepositoryQuestion repositoryQuestion,
            IRepositoryQuestionSubCategory repositoryQuestionSubCategory,
            IRepositoryAnswerVoting repositoryAnswerVoting)
        {
            _repositoryUser = repositoryUser;
            _repositoryAnswer = repositoryAnswer;
            _repositoryQuestion = repositoryQuestion;
            _repositoryQuestionSubCategory = repositoryQuestionSubCategory;
            _repositoryAnswerVoting = repositoryAnswerVoting;
        }
        public IList<UserActivityReportResponse> GetUsersActivityReport(FilterUsersActivityReport filter)
        {
            var report = new List<UserActivityReportResponse>();

            var users = _repositoryUser.FindAll();

            if (filter.CityId.HasValue)
                users = users.Where(x => x.CityId == filter.CityId.Value);
            else if(filter.CountryId.HasValue)
                users = users.Where(x => x.City.Country.ID == filter.CountryId.Value);

            foreach (var user in users)
            {
                var questions = _repositoryQuestion.FindByUser(user.ID).ToList();
                var tempQuestions = _repositoryQuestion.FindByUser(user.ID).ToList();

                foreach (var question in questions)
                {
                    var questionSubCategories = _repositoryQuestionSubCategory.Find(question.ID);

                    if (filter.SubCategoryId.HasValue)
                    {
                        if (!questionSubCategories.Any(x => x.SubCategoryId == filter.SubCategoryId.Value))
                            tempQuestions.Remove(question);
                    }
                    else if (filter.CategoryId.HasValue)
                    {
                        if (!questionSubCategories.Any(x => x.SubCategory.Category.ID == filter.CategoryId.Value))
                            tempQuestions.Remove(question);
                    }
                       
                }

                var answers = _repositoryAnswer.FindAllByUser(user.ID).ToList();
                var tempAnswers = _repositoryAnswer.FindAllByUser(user.ID).ToList();

                foreach (var answer in answers)
                {
                    var questionSubCategories = _repositoryQuestionSubCategory.Find(answer.QuestionId);

                    if (filter.SubCategoryId.HasValue)
                    {
                        if (!questionSubCategories.Any(x => x.SubCategoryId == filter.SubCategoryId.Value))
                            tempAnswers.Remove(answer);
                    }
                    else if (filter.CategoryId.HasValue)
                    {
                        if (!questionSubCategories.Any(x => x.SubCategory.Category.ID == filter.CategoryId.Value))
                            tempAnswers.Remove(answer);
                    }
                }

                var item = new UserActivityReportResponse { UserId = user.ID, UserName = user.Username, NumberOfQuestions = tempQuestions.Count, NumberOfAnswers = tempAnswers.Count };
                report.Add(item);
            }

            return report;
        }

        public IList<UserPerformanceReportResponse> GetUsersPerformanceReport(FilterUsersActivityReport filter)
        {
            var report = new List<UserPerformanceReportResponse>();

            var users = _repositoryUser.FindAll();

            if (filter.CityId.HasValue)
                users = users.Where(x => x.CityId == filter.CityId.Value);
            else if (filter.CountryId.HasValue)
                users = users.Where(x => x.City.Country.ID == filter.CountryId.Value);

            foreach (var user in users)
            {
                var answers = _repositoryAnswer.FindAllByUser(user.ID).ToList();
                var tempAnswers = _repositoryAnswer.FindAllByUser(user.ID).ToList();

                foreach (var answer in answers)
                {
                    var questionSubCategories = _repositoryQuestionSubCategory.Find(answer.QuestionId);

                    if (filter.SubCategoryId.HasValue)
                    {
                        if (!questionSubCategories.Any(x => x.SubCategoryId == filter.SubCategoryId.Value))
                            tempAnswers.Remove(answer);
                    }
                    else if (filter.CategoryId.HasValue)
                    {
                        if (!questionSubCategories.Any(x => x.SubCategory.Category.ID == filter.CategoryId.Value))
                            tempAnswers.Remove(answer);
                    }
                }

                decimal correctAnswerPercentage = 0.00M;
                if (tempAnswers.Count > 0)
                    correctAnswerPercentage = Math.Round(tempAnswers.Where(x => x.Accepted).Count() / Decimal.Parse(tempAnswers.Count.ToString()), 2) * 100;

                var item = new UserPerformanceReportResponse { UserId = user.ID, UserName = user.Username, TotalAnswers = tempAnswers.Count, TotalCorrectAnswers = tempAnswers.Where(x => x.Accepted).Count(), AnswerRate = correctAnswerPercentage };
                report.Add(item);
            }

            return report;
        }
    }
}
