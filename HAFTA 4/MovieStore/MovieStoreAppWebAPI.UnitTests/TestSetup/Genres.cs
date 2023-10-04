using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;

namespace MovieStoreAppWebAPI.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this IMovieStoreDbContext context)
        {


            context.Genres.AddRange(
                new Genre() { Name = "Action", Id = 1 },
                new Genre() { Name = "Adventure", Id = 2 },
                new Genre() { Name = "Comedy", Id = 3 },
                new Genre() { Name = "Science fiction", Id = 4 },
                new Genre() { Name = "Crime and mystery", Id = 5 },
                new Genre() { Name = "Fantasy", Id = 6 },
                new Genre() { Name = "Historical", Id = 7 },
                new Genre() { Name = "Horror", Id = 8 },
                new Genre() { Name = "Romance", Id = 9 },
                new Genre() { Name = "Speculative", Id = 10 }
                );

            context.SaveChanges();
        }
    }
}
