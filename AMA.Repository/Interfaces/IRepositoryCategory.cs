using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryCategory
    {
        IEnumerable<Category> FindAll();
        void Insert(Category category);
        Category TryFind(int id);
        Category TryFind(string name);
        void Delete(Category category);
    }
}
