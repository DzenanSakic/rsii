using AMA.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AMA.Repositories
{
    public class MSSQLDbContext : DbContext
    {
        public MSSQLDbContext(DbContextOptions<MSSQLDbContext> options) : base(options)
        {
        }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionVoting>().HasKey(vf => new { vf.UserId, vf.QuestionId });
            modelBuilder.Entity<AnswerVoting>().HasKey(vf => new { vf.UserId, vf.AnswerId });
            modelBuilder.Entity<UserSubCategory>().HasKey(vf => new { vf.UserId, vf.SubCategoryId });
            modelBuilder.Entity<UserCategory>().HasKey(vf => new { vf.UserId, vf.CategoryId });
            modelBuilder.Entity<QuestionSubCategory>().HasKey(vf => new { vf.QuestionId, vf.SubCategoryId });
            modelBuilder.Entity<UserFollow>().HasKey(vf => new { vf.UserFollowingId, vf.FollowedUserId });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSubCategory> UsersSubCategories { get; set; }
        public DbSet<UserCategory> UsersCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerVoting> AnswerVotings { get; set; }
        public DbSet<QuestionVoting> QuestionVotings { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<QuestionSubCategory> QuestionsSubCategories { get; set; }
        public DbSet<UserRole> UsersRole { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }
    }
}
