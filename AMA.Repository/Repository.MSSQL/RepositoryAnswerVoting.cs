using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryAnswerVoting : IRepositoryAnswerVoting
    {
        private readonly DbSet<AnswerVoting> _answerVotingContext;
        private readonly MSSQLDbContext _context;

        public RepositoryAnswerVoting(MSSQLDbContext context)
        {
            _context = context;
            _answerVotingContext = context.AnswerVotings;
        }

        public void Delete(AnswerVoting answerVoting)
        {
            _answerVotingContext.Remove(answerVoting);
            _context.SaveChanges();
        }

        public AnswerVoting FindByUserForAnswer(int userId, int answerId)
        {
            return _answerVotingContext.Where(x => x.AnswerId == answerId && x.UserId == userId).FirstOrDefault();
        }

        public IEnumerable<AnswerVoting> GetAll(int answerId)
        {
            return _answerVotingContext.Where(x => x.AnswerId == answerId).ToList();
        }

        public AnswerVoting Insert(AnswerVoting answerVoting)
        {
            _answerVotingContext.Add(answerVoting);
            _context.SaveChanges();

            return answerVoting;
        }

        public void Update(AnswerVoting answerVoting)
        {
            _answerVotingContext.Update(answerVoting);
            _context.SaveChanges();
        }
    }
}
