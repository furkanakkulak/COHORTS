using Microsoft.EntityFrameworkCore;
using webapi;

namespace webapi.DBOperations
{
    public class BookStoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "BookStoreDB");
        }

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }

        public List<Book> GetBooksFromDatabase()
        {
            return Books.ToList();
        }
    }
}
