using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryAnswer : IRepositoryAnswer
    {
        private readonly DbSet<Answer> _answerContext;
        private readonly MSSQLDbContext _context;
        public RepositoryAnswer(MSSQLDbContext context)
        {
            _answerContext = context.Answers;
            _context = context;
        }
        public void Delete(Answer answer)
        {
            _answerContext.Remove(answer);
            _context.SaveChanges();
        }

        public Answer Find(int id)
        {
            return _answerContext.Find(id);
        }

        public IEnumerable<Answer> FindAll(int questionId)
        {
            return _answerContext.Include(a => a.User).Where(x => x.QuestionId == questionId).ToList();
        }

        public IEnumerable<Answer> FindAllByUser(int userId)
        {
            return _answerContext.Where(x => x.UserId == userId).ToList();
        }

        public Answer Insert(Answer answer)
        {
            _answerContext.Add(answer);
            _context.SaveChanges();

            return _answerContext.Include(a => a.User).Where(x => x.ID == answer.ID).FirstOrDefault();
        }

        public Answer Update(Answer answer)
        {
            _answerContext.Update(answer);
            _context.SaveChanges();

            return answer;
        }
    }
}
