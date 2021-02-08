using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryQuestionSubCategory : IRepositoryQuestionSubCategory
    {
        private readonly DbSet<QuestionSubCategory> _questionSubCategoriesContext;
        private readonly MSSQLDbContext _context;
        public RepositoryQuestionSubCategory(MSSQLDbContext context)
        {
            _questionSubCategoriesContext = context.QuestionsSubCategories;
            _context = context;
        }

        public IEnumerable<QuestionSubCategory> Find(int quesitonId)
        {
            return _questionSubCategoriesContext.Include(a => a.SubCategory).ThenInclude(c => c.Category).Where(x=> x.QuestionId == quesitonId).ToList();
        }

        public void Delete(QuestionSubCategory questionSubCategory)
        {
            _questionSubCategoriesContext.Remove(questionSubCategory);
            _context.SaveChanges();
        }

        public void Insert(QuestionSubCategory questionSubCategory)
        {
            _questionSubCategoriesContext.Add(questionSubCategory);
            _context.SaveChanges();
        }
    }
}
