using AMA.Models.Entities;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryUserRole
    {
        UserRole Get(int userId);
        void Insert(UserRole userRole);
    }
}
