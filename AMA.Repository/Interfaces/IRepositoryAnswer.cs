using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryAnswer
    {
        Answer Insert(Answer answer);
        void Delete(Answer answer);
        IEnumerable<Answer> FindAll(int questionId);
        Answer Find(int id);
        Answer Update(Answer answer);
    }
}
