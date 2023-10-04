using AutoMapper;
using FluentAssertions;
using webapi.Applications.GenreOperations.Commands.CreateGenre;
using webapi.DBOperations;
using webapi.Entities;
using webapi.UnitTests.TestSetup;
using Xunit;

namespace webapi.UnitTests.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExitGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre()
            {
                Name = "WhenAlreadyExitGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel() { Name = genre.Name };

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Genre is already exist!");
        }

        [Fact]
        public void WhenValidInputsAreaGiven_Genre_shouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel()
            {
                Name = "WhenValidInputIsGiven_ShouldBeCreated"
            };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == command.Model.Name);
            genre.Should().NotBeNull();
        }
    }
}
