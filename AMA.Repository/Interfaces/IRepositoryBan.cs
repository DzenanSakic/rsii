using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryBan
    {
        void Insert(Ban ban);
        Ban TryFind(int id);
        IEnumerable<Ban> FindAllByUserId(int userid);
        void Update(Ban activeBan);
    }
}
