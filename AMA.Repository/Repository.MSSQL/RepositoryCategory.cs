using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryCategory : IRepositoryCategory
    {
        private readonly DbSet<Category> _categoryContext;
        private readonly MSSQLDbContext _context;
        public RepositoryCategory(MSSQLDbContext context)
        {
            _context = context;
            _categoryContext = context.Categories;
        }
        public Category TryFind(int id)
        {
            return _categoryContext.Find(id);
        }

        public void Insert(Category category)
        {
            _categoryContext.Add(category);
            _context.SaveChanges();
        }

        public Category TryFind(string name)
        {
            return _categoryContext.Where(x => x.Name == name).FirstOrDefault();
        }

        public IEnumerable<Category> FindAll()
        {
            return _categoryContext.ToList();
        }

        public void Delete(Category category)
        {
            _categoryContext.Remove(category);
            _context.SaveChanges();
        }
    }
}
