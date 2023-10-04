using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Create;
using MovieStoreAppWebAPI.UnitTests.Helpers;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.PlayerOperations.Create
{
    public class CreatePlayerCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreatePlayerCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenSavedPlayerNameGiven_InvalidOperationException_ShouldBeThrew()
        {
            CreatePlayerCommand command = new CreatePlayerCommand(_context,_mapper)
            {
                Model = new CreatePlayerViewModel()
                {
                    Name = "Christian",
                    Surname = "Bale"
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Should().Throw<InvalidOperationException>().And.Message.Should().Be(Messages.PlayerAlreadyExists);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Player_ShouldBeCreated()
        {
            CreatePlayerCommand command = new CreatePlayerCommand(_context,_mapper)
            {
                Model = new CreatePlayerViewModel()
                {
                    Name = StringHelper.GenerateString(20),
                    Surname = StringHelper.GenerateString(20)
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Invoke();

            Player searchedPlayer = _context.Players.ToList().Find(x => x.Name == command.Model.Name && x.Surname == command.Model.Surname);
            searchedPlayer.Should().NotBeNull();
            searchedPlayer.Name.Should().Be(command.Model.Name);
            searchedPlayer.Surname.Should().Be(command.Model.Surname);
        }

    }
}
