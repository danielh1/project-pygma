using Microsoft.EntityFrameworkCore;
using Pygma.Data.Domain.Entities;

namespace Pygma.Data
{
    public class PygmaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> BlogPostComments { get; set; }
        public DbSet<Log> IncidentLogs { get; set; }

        public PygmaDbContext(DbContextOptions<PygmaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email).IsUnique();
        }
    }

}
