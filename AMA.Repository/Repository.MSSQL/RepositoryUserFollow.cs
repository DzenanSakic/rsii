using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryUserFollow : IRepositoryUserFollow
    {
        private readonly MSSQLDbContext _context;
        private readonly DbSet<UserFollow> _userFollowsContext;
        public RepositoryUserFollow(MSSQLDbContext context)
        {
            _context = context;
            _userFollowsContext = context.UserFollows;
        }
        public void Insert(UserFollow userFollow)
        {
            _userFollowsContext.Add(userFollow);
            _context.SaveChanges();
        }

        public void Remove(UserFollow userFollow)
        {
            _userFollowsContext.Remove(userFollow);
            _context.SaveChanges();
        }

        public UserFollow TryFind(int userFollowId, int followedUserId)
        {
            return _userFollowsContext.Where(x => x.FollowedUserId == followedUserId && x.UserFollowingId == userFollowId).FirstOrDefault();
        }

        public IEnumerable<UserFollow> TryFindAll(int userId)
        {
            return _userFollowsContext.Where(x => x.UserFollowingId == userId).ToList();
        }
    }
}
