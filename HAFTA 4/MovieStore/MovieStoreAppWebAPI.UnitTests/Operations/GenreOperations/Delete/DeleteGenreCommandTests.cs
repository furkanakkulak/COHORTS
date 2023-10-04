using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.GenreOperation.Create;
using MovieStoreAppWebAPI.Operations.GenreOperation.Delete;
using MovieStoreAppWebAPI.Operations.GenreOperation.Read;
using MovieStoreAppWebAPI.UnitTests.Helpers;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.GenreOperations.Delete
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteGenreCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdWasGiven_EntityNullException_ShouldBeThrown()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context)
            {
                Model = new DeleteGenreViewModel()
                {
                    Id = 999
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Should().Throw<EntityNullException>().And.Message.Should()
                .Be(Messages.GenreDoesNotExist);
        }

        [Fact]
        public void WhenValidIdWasGiven_Genre_ShouldBeDeleted()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context)
            {
                Model = new DeleteGenreViewModel()
                {
                    Id = 1
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Invoke();

            _context.Genres.ToList().Find(x => x.Id == command.Model.Id).Should().BeNull();

        }
    }
}
