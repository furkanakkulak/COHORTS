using AutoMapper;

using Microsoft.EntityFrameworkCore;

using MovieStoreAppWebAPI.Common;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;

namespace MovieStoreAppWebAPI.UnitTests.TestSetup
{
    public class CommonTextFixture
    {
        public MovieStoreInMemoryDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTextFixture()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapProfile>();
            }).CreateMapper();

            var options = new DbContextOptionsBuilder<MovieStoreInMemoryDbContext>().UseInMemoryDatabase("name : MovieStoreTestDb").Options;

            Context = new MovieStoreInMemoryDbContext(options);

            Context.Database.EnsureCreated();

            Context.AddGenres();
            Context.AddDirectors();
            Context.AddPlayers();
            Context.AddFilms();
        }
    }
}
