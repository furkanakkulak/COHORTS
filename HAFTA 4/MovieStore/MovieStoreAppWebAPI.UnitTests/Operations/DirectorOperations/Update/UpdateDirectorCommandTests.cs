using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Update;
using MovieStoreAppWebAPI.UnitTests.Helpers;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.DirectorOperations.Update
{
    public class UpdateDirectorCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateDirectorCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdWasGiven_EntityNullException_ShouldBeThrown()
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context)
            {
                Model = new UpdateDirectorViewModel()
                {
                    Id = 999
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Should().Throw<EntityNullException>().And.Message.Should()
                .Be(Messages.DirectorDoesNotExist);
        }

        [Fact]
        public void WhenValidIdWasGiven_Director_ShouldBeUpdated()
        {

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context)
            {
                Model = new UpdateDirectorViewModel()
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

            Director searchedDirector = _context.Directors.ToList().Find(x => x.Id == command.Model.Id);
            searchedDirector.Should().NotBeNull();
            searchedDirector.Name.Should().Be(command.Model.Name);
            searchedDirector.Surname.Should().Be(command.Model.Surname);
        }
    }
}
