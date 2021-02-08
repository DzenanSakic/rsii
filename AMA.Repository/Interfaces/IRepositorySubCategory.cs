using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositorySubCategory
    {
        SubCategory TryFind(string name, int categortyId);
        void Insert(SubCategory subCategory);
        SubCategory TryFind(int id);
        SubCategory TryFind(string name);
        IEnumerable<SubCategory> TryFindAll(int categoryId);
        void Delete(SubCategory subCategory);
    }
}
