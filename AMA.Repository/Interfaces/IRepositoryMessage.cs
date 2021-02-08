using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryMessage
    {
        IEnumerable<Message> FindByUser(int userId);
        void Insert(Message message);
    }
}
