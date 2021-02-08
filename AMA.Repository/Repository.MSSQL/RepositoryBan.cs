using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryBan : IRepositoryBan
    {
        private readonly DbSet<Ban> _banContext; 
        private readonly MSSQLDbContext _context;
        public RepositoryBan(MSSQLDbContext context)
        {
            _banContext = context.Bans;
            _context = context;
        }

        public IEnumerable<Ban> FindAllByUserId(int userid)
        {
            return _banContext.Where(x => x.UserId == userid).ToList();
        }

        public void Insert(Ban ban)
        {
            _banContext.Add(ban);
            _context.SaveChanges();
        }

        public Ban TryFind(int id)
        {
            return _banContext.Find(id);
        }

        public void Update(Ban activeBan)
        {
            _banContext.Update(activeBan);
            _context.SaveChanges();
        }
    }
}
