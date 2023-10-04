using AutoMapper;

using FluentAssertions;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Delete;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.DirectorOperations.Delete
{
    public class DeleteDirectorCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteDirectorCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidIdWasGiven_EntityNullException_ShouldBeThrown()
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context)
            {
                Model = new DeleteDirectorViewModel()
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
        public void WhenValidIdWasGiven_Director_ShouldBeDeleted()
        {

            DeleteDirectorCommand mainCommand = new DeleteDirectorCommand(_context)
            {
                Model = new DeleteDirectorViewModel()
                {
                    Id = 1
                }
            };

            FluentActions.Invoking(() =>
            {
                mainCommand.Handle();
            }).Invoke();

            _context.Directors.ToList().Find(x => x.Id == mainCommand.Model.Id).Should().BeNull();
        }
    }
}
