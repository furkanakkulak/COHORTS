using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Create;
using MovieStoreAppWebAPI.UnitTests.Helpers;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.DirectorOperations.Create
{
    public class CreateDirectorCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenSavedDirectorNameGiven_InvalidOperationException_ShouldBeThrew()
        {
            CreateDirectorCommand command = new CreateDirectorCommand(_context,_mapper)
            {
                Model = new CreateDirectorViewModel()
                {
                    Name = "Christopher",
                    Surname = "Nolan"
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Should().Throw<InvalidOperationException>().And.Message.Should().Be(Messages.DirectorAlreadyExists);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeCreated()
        {
            CreateDirectorCommand command = new CreateDirectorCommand(_context,_mapper)
            {
                Model = new CreateDirectorViewModel()
                {
                    Name = "Buster",
                    Surname = "Keaton"
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Invoke();

            Director searchedDirector = _context.Directors.ToList().Find(x => x.Name == command.Model.Name && x.Surname == command.Model.Surname);
            searchedDirector.Should().NotBeNull();
            searchedDirector.Name.Should().Be(command.Model.Name);
            searchedDirector.Surname.Should().Be(command.Model.Surname);
        }

    }
}
