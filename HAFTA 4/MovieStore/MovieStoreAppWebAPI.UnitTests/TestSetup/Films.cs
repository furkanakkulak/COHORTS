using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;

namespace MovieStoreAppWebAPI.UnitTests.TestSetup
{
    public static class Films
    {
        public static void AddFilms(this IMovieStoreDbContext context)
        {

            context.Films.AddRange(new Film()
            {
                Id = 1,
                Director = context.Directors.Single(x => x.Id == 1),
                Genre = context.Genres.Single(x => x.Id == 1),
                Name = "Interstellar",
                Price = 200,
                PublishedDate = new DateTime(2015, 8, 26),
                About = "When Earth becomes uninhabitable in the future, a farmer and ex-NASA pilot, Joseph Cooper, is tasked to pilot a spacecraft, along with a team of researchers, to find a new planet for humans.",
                Players = new List<Player>()
                {
                    context.Players.Single(x => x.Id == 1),
                    context.Players.Single(x => x.Id == 2),
                }
            });

            context.Films.AddRange(new Film()
            {
                Id = 2,
                Director = context.Directors.Single(x => x.Id == 1),
                Genre = context.Genres.Single(x => x.Id == 1),
                Name = "Tenet",
                Price = 200,
                PublishedDate = new DateTime(2016, 8, 26),
                About = "When Earth becomes uninhabitable in the future, a farmer and ex-NASA pilot, Joseph Cooper, is tasked to pilot a spacecraft, along with a team of researchers, to find a new planet for humans.",
                Players = new List<Player>()
                {
                    context.Players.Single(x => x.Id == 3),
                    context.Players.Single(x => x.Id == 4),
                }
            });

            context.Films.AddRange(new Film()
            {
                Id = 3,
                Director = context.Directors.Single(x => x.Id == 2),
                Genre = context.Genres.Single(x => x.Id == 2),
                Name = "Inception",
                Price = 200,
                PublishedDate = new DateTime(2017, 8, 26),
                About = "When Earth becomes uninhabitable in the future, a farmer and ex-NASA pilot, Joseph Cooper, is tasked to pilot a spacecraft, along with a team of researchers, to find a new planet for humans.",
                Players = new List<Player>()
                {
                    context.Players.Single(x => x.Id == 3),
                    context.Players.Single(x => x.Id == 4),
                }
            });

            context.Films.AddRange(new Film()
            {
                Id = 4,
                Director = context.Directors.Single(x => x.Id == 2),
                Genre = context.Genres.Single(x => x.Id == 2),
                Name = "Taxi Driver",
                Price = 200,
                PublishedDate = new DateTime(2018, 8, 26),
                About = "When Earth becomes uninhabitable in the future, a farmer and ex-NASA pilot, Joseph Cooper, is tasked to pilot a spacecraft, along with a team of researchers, to find a new planet for humans.",
                Players = new List<Player>()
                {
                    context.Players.Single(x => x.Id == 1),
                    context.Players.Single(x => x.Id == 4),
                }
            });

            context.Films.AddRange(new Film()
            {
                Id = 5,
                Director = context.Directors.Single(x => x.Id == 3),
                Genre = context.Genres.Single(x => x.Id == 3),
                Name = "Quiet Place",
                Price = 200,
                PublishedDate = new DateTime(2019, 8, 26),
                About = "When Earth becomes uninhabitable in the future, a farmer and ex-NASA pilot, Joseph Cooper, is tasked to pilot a spacecraft, along with a team of researchers, to find a new planet for humans.",
                Players = new List<Player>()
                {
                    context.Players.Single(x => x.Id == 1),
                    context.Players.Single(x => x.Id == 2),
                }
            });

            context.SaveChanges();
        }
    }
}
