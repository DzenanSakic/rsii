using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryUserSubCategory
    {
        void Insert(UserSubCategory userSubCategory);
        IEnumerable<UserSubCategory> FindByUserId(int userId);
        UserSubCategory Find(int userId, int subCategoryId);
        void Delete(UserSubCategory userSubCategory);
        IEnumerable<UserSubCategory> TryFindAll(int userId);
    }
}
