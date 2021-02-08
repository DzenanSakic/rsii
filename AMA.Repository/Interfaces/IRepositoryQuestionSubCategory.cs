using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryQuestionSubCategory
    {
        void Insert(QuestionSubCategory questionSubCategory);
        IEnumerable<QuestionSubCategory> Find(int quesitonId);
        void Delete(QuestionSubCategory questionSubCategory);
    }
}
