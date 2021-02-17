using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryUserCategory : IRepositoryUserCategory
    {
        private readonly MSSQLDbContext _context;
        private readonly DbSet<UserCategory> _userCategoriesContext;
        public RepositoryUserCategory(MSSQLDbContext context)
        {
            _context = context;
            _userCategoriesContext = context.UsersCategories;
        }
        public void Delete(UserCategory userCategory)
        {
            _userCategoriesContext.Remove(userCategory);
            _context.SaveChanges();
        }

        public void Insert(UserCategory userCategory)
        {
            _userCategoriesContext.Add(userCategory);
            _context.SaveChanges();
        }

        public UserCategory TryFind(int userId, int categoryId)
        {
            return _userCategoriesContext.Where(x => x.CategoryId == categoryId && x.UserId == userId).FirstOrDefault();
        }

        public IList<UserCategory> TryFindAll(int userId)
        {
            return _userCategoriesContext.Where(x => x.UserId == userId).ToList();
        }
    }
}
