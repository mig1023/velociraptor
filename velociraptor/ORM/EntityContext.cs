using Microsoft.EntityFrameworkCore;
using velociraptor.Model;

namespace velociraptor.ORM
{
    public class EntityContext : DbContext
    {
        public EntityContext() => Database.EnsureCreated();

        public DbSet<Article> Articles { get; set; }

        public DbSet<History> Histories { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Velociraptor;Trusted_Connection=True;");
        }
    }
}
