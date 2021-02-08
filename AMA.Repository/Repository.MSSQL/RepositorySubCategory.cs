using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositorySubCategory : IRepositorySubCategory
    {
        private readonly DbSet<SubCategory> _subCategoryContext;
        private readonly MSSQLDbContext _context;
        public RepositorySubCategory(MSSQLDbContext context)
        {
            _context = context;
            _subCategoryContext = context.SubCategories;
        }

        public void Delete(SubCategory subCategory)
        {
            _subCategoryContext.Remove(subCategory);
            _context.SaveChanges();
        }

        public void Insert(SubCategory subCategory)
        {
            _subCategoryContext.Add(subCategory);
            _context.SaveChanges();
        }

        public SubCategory TryFind(int id)
        {
            return _subCategoryContext.Find(id);
        }

        public SubCategory TryFind(string name)
        {
            return _subCategoryContext.Where(x => x.Name == name).FirstOrDefault();
        }

        public SubCategory TryFind(string name, int categortyId)
        {
            return _subCategoryContext.Where(x => x.Name == name && x.CategoryId == categortyId).FirstOrDefault();
        }

        public IEnumerable<SubCategory> TryFindAll(int categoryId)
        {
            return _subCategoryContext.Where(x => x.CategoryId == categoryId).ToList();
        }
    }
}
