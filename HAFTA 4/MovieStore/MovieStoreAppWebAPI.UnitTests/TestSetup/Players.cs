using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;

namespace MovieStoreAppWebAPI.UnitTests.TestSetup
{
    public static class Players
    {
        public static void AddPlayers(this IMovieStoreDbContext context)
        {

            context.Players.AddRange(
                new Player() { Name = "Elizabeth", Surname = "Olsen", Id = 1 },
                new Player() { Name = "Scarlett", Surname = "Johansson", Id = 2 },
                new Player() { Name = "Christian", Surname = "Bale", Id = 3 },
                new Player() { Name = "Aaron", Surname = "Eckhart", Id = 4 },
                new Player() { Name = "Natalie", Surname = "Portman", Id = 5 },
                new Player() { Name = "Julianne", Surname = "Moore", Id = 6 },
                new Player() { Name = "Meghan", Surname = "Markle", Id = 7 },
                new Player() { Name = "Frank", Surname = "Ocean", Id = 8 }

                
                );

            context.SaveChanges();
        }
    }
}
