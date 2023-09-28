using Microsoft.EntityFrameworkCore;
using webapi.Entities;
using webapi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (
            var context = new BookStoreDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()
            )
        )
        {
            if (context.Books.Any())
            {
                return; // data seeded
            }

            context.Genres.AddRange(
                new Genre { Name = "Personal Growth", },
                new Genre { Name = "Science Fiction", },
                new Genre { Name = "Romance", }
            );

            context.Books.AddRange(
                new Book
                {
                    Title = "Lean Startup",
                    GenreId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12),
                    AuthorId = 1,
                },
                new Book
                {
                    Title = "Herland",
                    GenreId = 2,
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 01, 14),
                    AuthorId = 2,
                },
                new Book
                {
                    Title = "Dune",
                    GenreId = 2,
                    PageCount = 540,
                    PublishDate = new DateTime(2001, 12, 21),
                    AuthorId = 3,
                }
            );

            context.Authors.AddRange(
                new Author
                {
                    FirstName = "Eric",
                    LastName = "Ries",
                    BirthDate = new DateTime(1978, 09, 04)
                },
                new Author
                {
                    FirstName = "Charlotte",
                    LastName = "Perkins Gilman",
                    BirthDate = new DateTime(1860, 07, 03)
                },
                new Author
                {
                    FirstName = "Frank",
                    LastName = "Herbert",
                    BirthDate = new DateTime(1920, 10, 08)
                }
            );

            context.SaveChanges();
        }
    }
}
