using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryPayment
    {
        IEnumerable<Payment> FindByUser(int userId);
        void Insert(Payment payment);
    }
}
