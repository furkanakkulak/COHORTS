using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.FilmOperation.Delete;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.FilmOperations.Delete
{
    public class DeleteFilmCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteFilmCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdWasGiven_EntityNullException_ShouldBeThrown()
        {
            DeleteFilmCommand command = new DeleteFilmCommand(_context)
            {
                Model = new DeleteFilmViewModel()
                {
                    Id = 999
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Should().Throw<EntityNullException>().And.Message.Should()
                .Be(Messages.FilmDoesNotExist);
        }

        [Fact]
        public void WhenValidIdWasGiven_Film_ShouldBeDeleted()
        {
            DeleteFilmCommand command = new DeleteFilmCommand(_context)
            {
                Model = new DeleteFilmViewModel()
                {
                    Id = 1
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Invoke();

            _context.Films.ToList().Find(x => x.Id == command.Model.Id).Should().BeNull();

        }
    }
}
