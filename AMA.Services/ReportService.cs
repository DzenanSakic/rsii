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
        private readonly IRepositoryCategory _repositoryCategory;
        private readonly IRepositorySubCategory _repositorySubCategory;
        public ReportService(IRepositoryUser repositoryUser, 
            IRepositoryAnswer repositoryAnswer, 
            IRepositoryQuestion repositoryQuestion,
            IRepositoryQuestionSubCategory repositoryQuestionSubCategory,
            IRepositoryCategory repositoryCategory,
            IRepositorySubCategory repositorySubCategory)
        {
            _repositoryUser = repositoryUser;
            _repositoryAnswer = repositoryAnswer;
            _repositoryQuestion = repositoryQuestion;
            _repositoryQuestionSubCategory = repositoryQuestionSubCategory;
            _repositoryCategory = repositoryCategory;
            _repositorySubCategory = repositorySubCategory;
        }

        public IList<CategoryUsageReport> GetCategorySubCategoryUsage(FilterMostUsedCategoriesSubCategories filter)
        {
            var report = new List<CategoryUsageReport>();
            var categories = _repositoryCategory.FindAll();

            var questions = _repositoryQuestion.FindAll();
            var questionsTemp = questions;

            if (filter.CityId.HasValue)
                questionsTemp = questionsTemp.Where(x => x.User.CityId == filter.CityId.Value);
            else if (filter.CountryId.HasValue)
                questionsTemp = questionsTemp.Where(x => x.User.City.Country.ID == filter.CountryId.Value);

            if (filter.Gender.HasValue && filter.Gender.Value >= 0)
                questionsTemp = questionsTemp.Where(x => (int)x.User.Gender == filter.Gender.Value);

            if (filter.Year.HasValue)
                questionsTemp = questionsTemp.Where(x => x.CreatedTime.Year == filter.Year.Value);

            foreach (var category in categories)
            {
                var item = new CategoryUsageReport(); 
                item.NumberOfAnswers = 0;
                item.NumberOfQuestions = 0;
                item.Name = category.Name;
                item.Id = category.ID;
                item.SubCategoryUsageReport = new List<SubCategoryUsageReport>();
               

                foreach (var question in questionsTemp)
                {
                    var questionSubCategory = _repositoryQuestionSubCategory.Find(question.ID).FirstOrDefault();
                    var answers = _repositoryAnswer.FindAll(question.ID);

                    if (questionSubCategory.SubCategory.CategoryId == category.ID)
                    {
                        item.NumberOfQuestions++;
                        item.NumberOfAnswers += answers.Count();

                        if (item.SubCategoryUsageReport.Any(x => x.Id == questionSubCategory.SubCategoryId))
                        {
                            var correctItem = item.SubCategoryUsageReport.FirstOrDefault(x => x.Id == questionSubCategory.SubCategoryId);
                            correctItem.NumberOfQuestions++;
                        }
                        else
                        {
                            var newItem = new SubCategoryUsageReport();
                            newItem.Id = questionSubCategory.SubCategoryId;
                            newItem.CategoryId = item.Id;
                            newItem.Name = questionSubCategory.SubCategory.Name;
                            newItem.NumberOfAnswers += answers.Count();
                            newItem.NumberOfQuestions = 1;

                            item.SubCategoryUsageReport.Add(newItem);
                        }

                    }
                }
                report.Add(item);
            }

            foreach (var item in report)
            {
                var subCategories = _repositorySubCategory.FindAll();
                foreach (var sc in subCategories)
                {
                    if(item.SubCategoryUsageReport.All(x => x.Id != sc.ID && x.CategoryId == sc.CategoryId) && item.Id == sc.CategoryId)
                    {
                        var newItem = new SubCategoryUsageReport();
                        newItem.Id = sc.ID;
                        newItem.CategoryId = item.Id;
                        newItem.Name = sc.Name;
                        newItem.NumberOfAnswers = 0;
                        newItem.NumberOfQuestions = 0;

                        item.SubCategoryUsageReport.Add(newItem);
                    }
                }
            }

            return report;
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
