using Microsoft.EntityFrameworkCore;

namespace velociraptor.ORM
{
    public class EntityContext : DbContext
    {
        public EntityContext() => Database.EnsureCreated();

        public DbSet<Article> Articles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Velociraptor;Trusted_Connection=True;");
        }
    }
}
