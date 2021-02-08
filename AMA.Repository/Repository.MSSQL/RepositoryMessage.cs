using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryMessage : IRepositoryMessage
    {
        private readonly DbSet<Message> _messagesContext;
        private readonly MSSQLDbContext _context;
        public RepositoryMessage(MSSQLDbContext context)
        {
            _messagesContext = context.Messages;
            _context = context;
        }

        public IEnumerable<Message> FindByUser(int userId)
        {
            return _messagesContext
                .Include(y => y.ToUser)
                .Include(a => a.FromUser)
                .Where(x => x.ToUserId == userId || x.FromUserId == userId)
                .ToList();
        }

        public void Insert(Message message)
        {
            _messagesContext.Add(message);
            _context.SaveChanges();
        }
    }
}
