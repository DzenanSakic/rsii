using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryUserFollow
    {
        void Remove(UserFollow userFollow);
        void Insert(UserFollow userFollow);
        IEnumerable<UserFollow> TryFindAll(int userId);
        UserFollow TryFind(int userFollowId, int followedUserId);
    }
}
