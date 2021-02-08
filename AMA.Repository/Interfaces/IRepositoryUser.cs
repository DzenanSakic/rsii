using AMA.Models.DTOS;
using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryUser
    {
        User TryFind(int id);
        User TryFind(string username);
        bool CheckMailAvailability(string mail);
        void Insert(User user);
        IEnumerable<User> FindAll(FindUsersRequest request = null);
        void Update(User user);
    }
}
