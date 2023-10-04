using Microsoft.EntityFrameworkCore;

using MovieStoreAppWebAPI.Entities;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovieStoreAppWebAPI.Operations.DatabaseOperation
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (
                var context = new MovieStoreInMemoryDbContext(
                    serviceProvider.GetRequiredService<
                        DbContextOptions<MovieStoreInMemoryDbContext>
                    >()
                )
            )
            {
                if (context.Films.Any())
                {
                    return;
                }

                if (context.Genres.Any())
                {
                    return;
                }

                if (context.Directors.Any())
                {
                    return;
                }

                if (context.Players.Any())
                {
                    return;
                }

                GenerateRoleEntities(context);
                GenerateGenreEntities(context);

                GenerateUserEntities(context);

                GenerateDirectorEntities(context);
                GeneratePlayerEntities(context);
                GenerateFilmEntities(context);

                GenerateOrderEntities(context);
            }
        }

        private static void GenerateRoleEntities(MovieStoreInMemoryDbContext context)
        {
            context.Roles.AddRange(
                new Role() { Id = 1, Name = "Admin" },
                new Role() { Id = 2, Name = "User" }
            );

            context.SaveChanges();
        }

        private static void GenerateOrderEntities(MovieStoreInMemoryDbContext context)
        {
            var order1 = new Order()
            {
                Id = 1,
                Film = context.Films.SingleOrDefault(x => x.Id == 1),
                IsDeleted = false,
                User = context.Users.SingleOrDefault(x => x.Id == 1)
            };

            var order2 = new Order()
            {
                Id = 2,
                Film = context.Films.SingleOrDefault(x => x.Id == 2),
                IsDeleted = false,
                User = context.Users.SingleOrDefault(x => x.Id == 1)
            };

            var order3 = new Order()
            {
                Id = 3,
                Film = context.Films.SingleOrDefault(x => x.Id == 1),
                IsDeleted = false,
                User = context.Users.SingleOrDefault(x => x.Id == 2)
            };

            context.Orders.AddRange(order1, order2, order3);

            context.SaveChanges();
        }

        private static void GenerateUserEntities(MovieStoreInMemoryDbContext context)
        {
            //Password : 12345

            var user1 = new User()
            {
                Id = 1,
                Name = "Furkan",
                Surname = "AKKULAK",
                Username = "furkanakkulak",
                Email = "akkulak.furkan26@gmail.com",
                // Byakko2626@ = password
                PasswordHash = "DZcInU8d5diUqjwi1ZDuBcKuvrC/HOnUAPCoVnfHxVU=",
                PasswordSalt = "GKq/dPcZyreY9VhhpiTYqUUJ+MT+GOCSaqw0BdBqpHY=",
                FavouriteGenres = new List<Genre>()
                {
                    context.Genres.SingleOrDefault(x => x.Id == 1),
                    context.Genres.SingleOrDefault(x => x.Id == 2),
                    context.Genres.SingleOrDefault(x => x.Id == 3),
                    context.Genres.SingleOrDefault(x => x.Id == 4),
                    context.Genres.SingleOrDefault(x => x.Id == 5),
                },
                Roles = new List<Role>() { context.Roles.SingleOrDefault(x => x.Id == 2), }
            };

            var user2 = new User()
            {
                Id = 2,
                Name = "Super",
                Surname = "Admin",
                Username = "superadmin",
                Email = "superadmin@moviestore.com",
                // Byakko2626@ = password
                PasswordHash = "DZcInU8d5diUqjwi1ZDuBcKuvrC/HOnUAPCoVnfHxVU=",
                PasswordSalt = "GKq/dPcZyreY9VhhpiTYqUUJ+MT+GOCSaqw0BdBqpHY=",
                Roles = new List<Role>() { context.Roles.SingleOrDefault(x => x.Id == 1), }
            };

            context.Users.AddRange(user1, user2);

            context.SaveChanges();
        }

        private static void GenerateFilmEntities(MovieStoreInMemoryDbContext context)
        {
            context.Films.AddRange(
                new Film()
                {
                    Id = 1,
                    Director = context.Directors.Single(x => x.Id == 1),
                    Genre = context.Genres.Single(x => x.Id == 4),
                    Name = "Tenet",
                    Price = 200,
                    PublishedDate = new DateTime(2020, 8, 26),
                    About =
                        "Tenet is a 2020 science fiction action thriller film written and directed by Christopher Nolan, who produced it with Emma Thomas. A co-production between the United Kingdom and United States, it stars John David Washington, Robert Pattinson, Elizabeth Debicki, Dimple Kapadia, Michael Caine, and Kenneth Branagh. The film follows a secret agent who learns to manipulate the flow of time to prevent an attack from the future that threatens to annihilate the present world.",
                    Players = new List<Player>()
                    {
                        context.Players.Single(x => x.Id == 14),
                        context.Players.Single(x => x.Id == 15),
                        context.Players.Single(x => x.Id == 16),
                        context.Players.Single(x => x.Id == 17),
                        context.Players.Single(x => x.Id == 18),
                    }
                },
                new Film()
                {
                    Id = 2,
                    Director = context.Directors.Single(x => x.Id == 1),
                    Genre = context.Genres.Single(x => x.Id == 4),
                    Name = "Interstellar",
                    Price = 200,
                    PublishedDate = new DateTime(2020, 8, 26),
                    About =
                        "When Earth becomes uninhabitable in the future, a farmer and ex-NASA pilot, Joseph Cooper, is tasked to pilot a spacecraft, along with a team of researchers, to find a new planet for humans.",
                    Players = new List<Player>()
                    {
                        context.Players.Single(x => x.Id == 13),
                        context.Players.Single(x => x.Id == 19),
                        context.Players.Single(x => x.Id == 20),
                        context.Players.Single(x => x.Id == 21),
                        context.Players.Single(x => x.Id == 22)
                    }
                }
            );
            ;

            context.SaveChanges();
        }

        private static void GeneratePlayerEntities(MovieStoreInMemoryDbContext context)
        {
            context.Players.AddRange(
                new Player()
                {
                    Name = "Elizabeth",
                    Surname = "Olsen",
                    Id = 1
                },
                new Player()
                {
                    Name = "Scarlett",
                    Surname = "Johansson",
                    Id = 2
                },
                new Player()
                {
                    Name = "Tom",
                    Surname = "Cruise",
                    Id = 3
                },
                new Player()
                {
                    Name = "Jim",
                    Surname = "Carrey",
                    Id = 4
                },
                new Player()
                {
                    Name = "Will",
                    Surname = "Smith",
                    Id = 5
                },
                new Player()
                {
                    Name = "Robert Downey",
                    Surname = "Jr.",
                    Id = 6
                },
                new Player()
                {
                    Name = "Johnny",
                    Surname = "Depp",
                    Id = 7
                },
                new Player()
                {
                    Name = "Brad",
                    Surname = "Pitt",
                    Id = 8
                },
                new Player()
                {
                    Name = "Angelina",
                    Surname = "Jolie",
                    Id = 9
                },
                new Player()
                {
                    Name = "Leonardo",
                    Surname = "DiCaprio",
                    Id = 10
                },
                new Player()
                {
                    Name = "Clint",
                    Surname = "Eastwood",
                    Id = 11
                },
                new Player()
                {
                    Name = "Hugh",
                    Surname = "Jackman",
                    Id = 12
                },
                new Player()
                {
                    Name = "Matt",
                    Surname = "Damon",
                    Id = 13
                },
                new Player()
                {
                    Name = "Elizabeth",
                    Surname = "Debicki",
                    Id = 14
                },
                new Player()
                {
                    Name = "Robert",
                    Surname = "Pattinson",
                    Id = 15
                },
                new Player()
                {
                    Name = "John David",
                    Surname = "Washington",
                    Id = 16
                },
                new Player()
                {
                    Name = "Kenneth",
                    Surname = "Branagh",
                    Id = 17
                },
                new Player()
                {
                    Name = "Clémence",
                    Surname = "Poésy",
                    Id = 18
                },
                new Player()
                {
                    Name = "Anne",
                    Surname = "Hathaway",
                    Id = 19
                },
                new Player()
                {
                    Name = "Jessica",
                    Surname = "Chastain",
                    Id = 20
                },
                new Player()
                {
                    Name = "Michael",
                    Surname = "Caine",
                    Id = 21
                },
                new Player()
                {
                    Name = "Matthew",
                    Surname = "McConaughey",
                    Id = 22
                },
                new Player()
                {
                    Name = "Heath",
                    Surname = "Ledger",
                    Id = 23
                },
                new Player()
                {
                    Name = "Christian",
                    Surname = "Bale",
                    Id = 24
                },
                new Player()
                {
                    Name = "Aaron",
                    Surname = "Eckhart",
                    Id = 25
                }
            );

            context.SaveChanges();
        }

        private static void GenerateGenreEntities(MovieStoreInMemoryDbContext context)
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

        private static void GenerateDirectorEntities(MovieStoreInMemoryDbContext context)
        {
            context.Directors.AddRange(
                new Director()
                {
                    Name = "Christopher",
                    Surname = "Nolan",
                    Id = 1
                },
                new Director()
                {
                    Name = "Quentin",
                    Surname = "Tarantino",
                    Id = 2
                },
                new Director()
                {
                    Name = "Michael",
                    Surname = "Bay",
                    Id = 3
                },
                new Director()
                {
                    Name = "Martin",
                    Surname = "Scorsese",
                    Id = 4
                }
            );

            context.SaveChanges();
        }
    }
}
