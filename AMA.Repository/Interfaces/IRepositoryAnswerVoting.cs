using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryAnswerVoting
    {
        IEnumerable<AnswerVoting> GetAll(int answerId);
        AnswerVoting FindByUserForAnswer(int userId, int answerId);
        void Delete(AnswerVoting answerVoting);
        void Update(AnswerVoting answerVoting);
        AnswerVoting Insert(AnswerVoting answerVoting);
    }
}
