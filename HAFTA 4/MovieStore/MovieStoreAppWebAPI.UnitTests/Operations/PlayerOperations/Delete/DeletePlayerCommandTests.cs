using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.FilmOperation.Read;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Create;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Delete;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Read;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Update;
using MovieStoreAppWebAPI.UnitTests.Helpers;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.PlayerOperations.Delete
{
    public class DeletePlayerCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeletePlayerCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdWasGiven_EntityNullException_ShouldBeThrown()
        {
            DeletePlayerCommand command = new DeletePlayerCommand(_context)
            {
                Model = new DeletePlayerViewModel()
                {
                    Id = 999
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Should().Throw<EntityNullException>().And.Message.Should()
                .Be(Messages.PlayerDoesNotExist);
        }

        [Fact]
        public void WhenValidIdWasGiven_PlayerActedNoFilms_ShouldBeDeleted()
        {
            DeletePlayerCommand mainCommand = new DeletePlayerCommand(_context)
            {
                Model = new DeletePlayerViewModel()
                {
                    Id = 4
                }
            };

            FluentActions.Invoking(() =>
            {
                mainCommand.Handle();
            }).Invoke();

            _context.Players.ToList().Find(x => x.Id == mainCommand.Model.Id).Should().BeNull();
        }

        [Fact]
        public void WhenValidIdWasGiven_PlayerActedFilms_ShouldBeDeleted()
        {

            DeletePlayerCommand mainCommand = new DeletePlayerCommand(_context)
            {
                Model = new DeletePlayerViewModel()
                {
                    Id = 1
                }
            };

            FluentActions.Invoking(() =>
            {
                mainCommand.Handle();
            }).Invoke();

            _context.Players.ToList().Find(x => x.Id == mainCommand.Model.Id).Should().BeNull();
        }
    }
}
