using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.DatabaseOperation.Configuration;

namespace MovieStoreAppWebAPI.Operations.DatabaseOperation
{
    public class MovieStoreInMemoryDbContext:DbContext,IMovieStoreDbContext
    {
        public MovieStoreInMemoryDbContext(DbContextOptions<MovieStoreInMemoryDbContext> options):base(options)
        {
            DatabaseInstance = this.Database;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MovieStoreAppDb");
            optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        public DatabaseFacade DatabaseInstance { get; }
        public DbSet<User> Users { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Film>  Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
