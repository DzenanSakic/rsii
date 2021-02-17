using AMA.Models.DTOS;
using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryQuestion : IRepositoryQuestion
    {
        private readonly DbSet<Question> _questionContext;
        private readonly MSSQLDbContext _context;
        public RepositoryQuestion(MSSQLDbContext context)
        {
            _questionContext = context.Questions;
            _context = context;
        }

        public void Delete(Question question)
        {
            _questionContext.Remove(question);
            _context.SaveChanges();
        }

        public IEnumerable<Question> FindAll()
        {
            return _questionContext.Include(x => x.User).ThenInclude(y => y.City).ThenInclude(a => a.Country).ToList();
        }

        public void Insert(Question question)
        {
            _questionContext.Add(question);
            _context.SaveChanges();
        }

        public Question TryFind(int id)
        {
            return _questionContext.Find(id);
        }

        public IEnumerable<Question> Find(FindQuestionsRequest request)
        {
            var questions = _questionContext.AsQueryable();

            if(!string.IsNullOrEmpty(request.Title))
            {
                questions = questions.Where(x => x.Title.Contains(request.Title));
            }

            if(request.SubCategoryId > 0)
            {
                questions = questions.Where(
                    x => _context.QuestionsSubCategories.Where(y => y.QuestionId == x.ID)
                    .Select(qs => qs.SubCategoryId)
                    .ToList()
                    .Contains(request.SubCategoryId)
                );
            }
            else if (request.CategoryId > 0)
            {
                questions = questions.Where(
                    x => _context.QuestionsSubCategories.Where(y => y.QuestionId == x.ID)
                    .Where(c => c.SubCategory.CategoryId == request.CategoryId)
                    .Any()
                );
            }

            return questions.Include(x => x.User).ToList();
        }

        public IEnumerable<Question> FindByUser(int userId)
        {
            return _questionContext.Where(x => x.UserId == userId).ToList();
        }
    }
}
