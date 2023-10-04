using AutoMapper;
using FluentAssertions;
using webapi.Applications.GenreOperations.Commands.UpdateGenre;
using webapi.DBOperations;
using webapi.Entities;
using webapi.UnitTests.TestSetup;
using Xunit;

namespace webapi.UnitTests.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIdIsNotinDB_InvalidOperationsException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 0;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Genre is not found!");
        }

        [Fact]
        public void WhenGivenNameIsSameWithAnotherGenre_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre() { Name = "Science Fiction" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 2;
            command.Model = new UpdateGenreModel() { Name = "Science Fiction" };

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Genre is already exist!");
        }

        [Fact]
        public void WhenGivenGenreIdinDB_ShouldBeUpdate()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);

            command.Model = new UpdateGenreModel() { Name = "WhenGivenGenreIdinDB_ShouldBeUpdate" };
            command.GenreId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().NotBeNull();
        }
    }
}
