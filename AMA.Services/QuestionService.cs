using AMA.Models.DTOS;
using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IRepositoryQuestion _repositoryQuestion;
        private readonly IRepositoryAnswer _repositoryAnswer;
        private readonly IRepositoryQuestionSubCategory _repositoryQuestionSubCategory;
        private readonly IRepositoryAnswerVoting _repositoryAnswerVoting;
        public QuestionService(IRepositoryQuestion repositoryQuestion, 
            IRepositoryQuestionSubCategory repositoryQuestionSubCategory,
            IRepositoryAnswer repositoryAnswer,
            IRepositoryAnswerVoting repositoryAnswerVoting)
        {
            _repositoryQuestion = repositoryQuestion;
            _repositoryQuestionSubCategory = repositoryQuestionSubCategory;
            _repositoryAnswer = repositoryAnswer;
            _repositoryAnswerVoting = repositoryAnswerVoting;
        }
        public void AddQuestion(InsertQuestionRequest request, int userId)
        {
            var question = new Question
            {
                Body = request.Body,
                Title = request.Title,
                UserId = userId,
                CreatedTime = DateTime.Now
            };

            _repositoryQuestion.Insert(question);

            foreach (var sc in request.SubCategories)
            {
                var questionSubCategory = new QuestionSubCategory
                {
                    QuestionId = question.ID,
                    SubCategoryId = sc
                };

                _repositoryQuestionSubCategory.Insert(questionSubCategory);
            }
        }

        public void Delete(Question question)
        {
            var answers = _repositoryAnswer.FindAll(question.ID);
            foreach (var answer in answers)
            {
                var answerVotings = _repositoryAnswerVoting.GetAll(answer.ID);

                foreach (var answerVoting in answerVotings)
                {
                    _repositoryAnswerVoting.Delete(answerVoting);
                } 

                _repositoryAnswer.Delete(answer);
            }

            var qSubCategories = _repositoryQuestionSubCategory.Find(question.ID);
            foreach (var qsc in qSubCategories)
            {
                _repositoryQuestionSubCategory.Delete(qsc);
            }

            _repositoryQuestion.Delete(question);
        }

        public void Edit(EditQuestionRequest request)
        {
            var question = _repositoryQuestion.TryFind(request.QuestionId);
            if(question != null)
            {
                var qSubCategory = _repositoryQuestionSubCategory.Find(question.ID).FirstOrDefault();
                if(request.SubCategoryId.HasValue)
                {
                    _repositoryQuestionSubCategory.Delete(qSubCategory);

                    qSubCategory.SubCategoryId = request.SubCategoryId.Value;
                    qSubCategory.SubCategory = null;

                    _repositoryQuestionSubCategory.Insert(qSubCategory);

                }

                if (!string.IsNullOrEmpty(request.Title))
                    question.Title = request.Title;

                if (!string.IsNullOrEmpty(request.Body))
                    question.Body = request.Body;
            }
        }
    }
}
