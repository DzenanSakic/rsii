using AMA.Models.DTOS;
using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryQuestion
    {
        Question TryFind(int id);
        IEnumerable<Question> FindAll();
        void Insert(Question question);
        void Delete(Question question);
        IEnumerable<Question> Find(FindQuestionsRequest request);
    }
}
