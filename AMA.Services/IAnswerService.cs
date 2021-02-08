using AMA.Models.DTOS;
using AMA.Models.Entities;

namespace AMA.Services
{
    public interface IAnswerService
    {
        Answer AddAnswer(InsertAnswerRequest request, int userId);
        AnswerVoting AddVoting(AddAnswerVotingRequest request);
        void Delete(Answer answer);
        Answer ChangeCorrectState(Answer answer);
    }
}
