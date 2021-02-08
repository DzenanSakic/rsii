using AMA.Models.DTOS;
using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using System;

namespace AMA.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IRepositoryAnswer _repositoryAnswer;
        private readonly IRepositoryAnswerVoting _repositoryAnswerVoting;
        public AnswerService(IRepositoryAnswer repositoryAnswer, IRepositoryAnswerVoting repositoryAnswerVoting)
        {
            _repositoryAnswer = repositoryAnswer;
            _repositoryAnswerVoting = repositoryAnswerVoting;
        }
        public Answer AddAnswer(InsertAnswerRequest request, int userId)
        {
            var answer = new Answer
            {
                Accepted = false,
                QuestionId = request.QuestionId,
                UserId = userId,
                Text = request.Text,
                TimeAnswered = DateTime.Now
            };

            return _repositoryAnswer.Insert(answer);
        }

        public AnswerVoting AddVoting(AddAnswerVotingRequest request)
        {
            var answerVoting = _repositoryAnswerVoting.FindByUserForAnswer(request.UserId, request.AnswerId);

            if(answerVoting != null)
            {
                if (answerVoting.Rating == request.Rating)
                {
                    _repositoryAnswerVoting.Delete(answerVoting);
                }
                else
                {
                    answerVoting.Rating = request.Rating;
                    _repositoryAnswerVoting.Update(answerVoting);
                }
            }
            else
            {
                var newVoting = new AnswerVoting() { AnswerId = request.AnswerId, UserId = request.UserId, Rating = request.Rating };
                answerVoting = _repositoryAnswerVoting.Insert(newVoting);
            }

            return answerVoting;
        }

        public Answer ChangeCorrectState(Answer answer)
        {
            answer.Accepted = !answer.Accepted;
            return _repositoryAnswer.Update(answer);
        }

        public void Delete(Answer answer)
        {
            var answerVotings = _repositoryAnswerVoting.GetAll(answer.ID);

            foreach (var answerVoting in answerVotings)
            {
                _repositoryAnswerVoting.Delete(answerVoting);
            }

            _repositoryAnswer.Delete(answer);
        }
    }
}
