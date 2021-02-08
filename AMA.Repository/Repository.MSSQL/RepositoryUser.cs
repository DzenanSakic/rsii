using AMA.Models.DTOS;
using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly DbSet<User> _userContext;
        private readonly MSSQLDbContext _context;
        public RepositoryUser(MSSQLDbContext context)
        {
            _userContext = context.Users;
            _context = context;
        }

        public bool CheckMailAvailability(string mail)
        {
            return _userContext.Where(x => x.Mail == mail).FirstOrDefault() == null;
        }

        public IEnumerable<User> FindAll(FindUsersRequest request = null)
        {
            var query = _userContext.Include(y => y.City).ThenInclude(a=> a.Country).AsQueryable();

            if (request == null)
            {
                return query.ToList();
            }

            if (request.Id.HasValue)
            {
                query = query.Where(x => x.ID == request.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                query = query.Where(x => x.Username.Contains(request.UserName));
            }

            if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                query = query.Where(x => x.FirstName.Contains(request.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                query = query.Where(x => x.LastName.Contains(request.LastName));
            } 

            return query.ToList();
        }

        public User TryFind(string username)
        {
            return _userContext.Include(y => y.City).ThenInclude(a => a.Country).Where(x => x.Username == username).FirstOrDefault();
        }

        public User TryFind(int id)
        {
            return _userContext.Include(y => y.City).ThenInclude(a => a.Country).Where(x => x.ID == id).FirstOrDefault();
        }

        public void Insert(User user)
        {
            _userContext.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _userContext.Update(user);
            _context.SaveChanges();
        }
    }
}
