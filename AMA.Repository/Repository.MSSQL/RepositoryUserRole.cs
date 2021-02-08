using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryUserRole : IRepositoryUserRole
    {
        private readonly DbSet<UserRole> _userRoleContext;
        private readonly MSSQLDbContext _context;
        public RepositoryUserRole(MSSQLDbContext context)
        {
            _userRoleContext = context.UsersRole;
            _context = context;
        }
        public UserRole Get(int userId)
        {
            return _userRoleContext.Where(x => x.UserId == userId).FirstOrDefault(); 
        }

        public void Insert(UserRole userRole)
        {
            _userRoleContext.Add(userRole);
            _context.SaveChanges();
        }
    }
}
