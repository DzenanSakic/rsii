using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryUserSubCategory : IRepositoryUserSubCategory
    {
        private readonly DbSet<UserSubCategory> _userSubCategoriesContext;
        private readonly MSSQLDbContext _context;
        public RepositoryUserSubCategory(MSSQLDbContext context)
        {
            _userSubCategoriesContext = context.UsersSubCategories;
            _context = context;
        }

        public void Delete(UserSubCategory userSubCategory)
        {
            _userSubCategoriesContext.Remove(userSubCategory);
            _context.SaveChanges();
        }

        public UserSubCategory Find(int userId, int subCategoryId)
        {
            return _userSubCategoriesContext.Where(x => x.UserId == userId && x.SubCategoryId == subCategoryId).FirstOrDefault();
        }

        public IEnumerable<UserSubCategory> FindByUserId(int userId)
        {
            return _userSubCategoriesContext.Where(x => x.UserId == userId).ToList();
        }

        public void Insert(UserSubCategory userSubCategory)
        {
            _userSubCategoriesContext.Add(userSubCategory);
            _context.SaveChanges();
        }

        public IEnumerable<UserSubCategory> TryFindAll(int userId)
        {
            return _userSubCategoriesContext.Where(x => x.UserId == userId).ToList();
        }
    }
}
