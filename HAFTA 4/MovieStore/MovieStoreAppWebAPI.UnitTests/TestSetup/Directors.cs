using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;

using System;

namespace MovieStoreAppWebAPI.UnitTests.TestSetup
{
    public static class Directors
    {
        public static void AddDirectors(this IMovieStoreDbContext context)
        {

            context.Directors.AddRange(
                new Director() { Name = "Christopher", Surname = "Nolan", Id = 1 },
                new Director() { Name = "Quentin", Surname = "Tarantino", Id = 2 },
            new Director() { Name = "John", Surname = "Ford", Id = 3 },
                new Director() { Name = "Howard", Surname = "Hawks", Id = 4 },
                new Director() { Name = "Alfred", Surname = "Hitchcock", Id = 5 },
                new Director() { Name = "Martin", Surname = "Scorsese", Id = 6 },
                new Director() { Name = "Akira", Surname = "Kurosawa", Id = 7 },
                new Director() { Name = "Norman", Surname = "Panama", Id = 8 }
                );

            context.SaveChanges();
        }
    }
}
