using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryPayment : IRepositoryPayment
    {
        private readonly DbSet<Payment> _paymentContext;
        private readonly MSSQLDbContext _context;
        public RepositoryPayment(MSSQLDbContext context)
        {
            _paymentContext = context.Payments;
            _context = context;
        }

        public IEnumerable<Payment> FindByUser(int userId)
        {
            return _paymentContext.Where(x => x.FromUserId == userId || x.ToUserId == userId).ToList();
        }

        public void Insert(Payment payment)
        {
            _paymentContext.Add(payment);
            _context.SaveChanges();
        }
    }
}
