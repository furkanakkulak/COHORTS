using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using MovieStoreAppWebAPI.Entities;

namespace MovieStoreAppWebAPI.Operations.DatabaseOperation
{
    public interface IMovieStoreDbContext
    {
        int SaveChanges();
        public DbSet<User> Users { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DatabaseFacade DatabaseInstance { get; }
    }
}
