using AMA.Models.DTOS;
using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Services
{
    public interface IQuestionService
    {
        void AddQuestion(InsertQuestionRequest request, int userId);
        void Delete(Question question);
        void Edit(EditQuestionRequest request);
        IEnumerable<Question> FindSuggested(int userId);
    }
}
