using AstraLingo.Models;
using Microsoft.EntityFrameworkCore;

namespace AstraLingo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<Mission> Missions { get; set; }
    }
}
