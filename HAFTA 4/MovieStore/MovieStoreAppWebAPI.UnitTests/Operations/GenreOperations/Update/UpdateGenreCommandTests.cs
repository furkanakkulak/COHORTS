using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.GenreOperation.Create;
using MovieStoreAppWebAPI.Operations.GenreOperation.Read;
using MovieStoreAppWebAPI.Operations.GenreOperation.Update;
using MovieStoreAppWebAPI.UnitTests.Helpers;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.GenreOperations.Update
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdWasGiven_EntityNullException_ShouldBeThrown()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context)
            {
                Model = new UpdateGenreViewModel()
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
        public void WhenValidIdWasGiven_Book_ShouldBeUpdated()
        {

            UpdateGenreCommand command = new UpdateGenreCommand(_context)
            {
                Model = new UpdateGenreViewModel()
                {
                    Id = 1,
                    Name = StringHelper.GenerateString(20)
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Invoke();

            Genre searchedGenre = _context.Genres.ToList().Find(x => x.Id == command.Model.Id);
            searchedGenre.Should().NotBeNull();
            searchedGenre.Name.Should().Be(command.Model.Name);
        }
    }
}
