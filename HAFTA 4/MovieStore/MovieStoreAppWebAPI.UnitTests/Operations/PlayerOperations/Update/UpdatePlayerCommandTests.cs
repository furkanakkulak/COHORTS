using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Create;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Read;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Update;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Update;
using MovieStoreAppWebAPI.UnitTests.Helpers;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.PlayerOperations.Update
{
    public class UpdatePlayerCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdatePlayerCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdWasGiven_EntityNullException_ShouldBeThrown()
        {
            UpdatePlayerCommand command = new UpdatePlayerCommand(_context)
            {
                Model = new UpdatePlayerViewModel()
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
        public void WhenValidIdWasGiven_Player_ShouldBeUpdated()
        {

            UpdatePlayerCommand command = new UpdatePlayerCommand(_context)
            {
                Model = new UpdatePlayerViewModel()
                {
                    Id = 1,
                    Name = StringHelper.GenerateString(20),
                    Surname = StringHelper.GenerateString(20)
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Invoke();

            Player searchedPlayer = _context.Players.ToList().Find(x => x.Id == command.Model.Id);
            searchedPlayer.Should().NotBeNull();
            searchedPlayer.Name.Should().Be(command.Model.Name);
            searchedPlayer.Name.Should().Be(command.Model.Surname);
        }
    }
}
