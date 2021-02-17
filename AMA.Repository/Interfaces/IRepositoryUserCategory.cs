using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryUserCategory
    {
        void Insert(UserCategory userCategory);
        void Delete(UserCategory userCategory);
        UserCategory TryFind(int userId, int categoryId);
        IList<UserCategory> TryFindAll(int userId);
    }
}
