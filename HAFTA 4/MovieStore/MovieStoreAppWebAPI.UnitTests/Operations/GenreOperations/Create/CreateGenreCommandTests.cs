using AutoMapper;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.GenreOperation.Create;
using MovieStoreAppWebAPI.UnitTests.Helpers;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.GenreOperations.Create
{
    public class CreateGenreCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenSavedGenreNameGiven_InvalidOperationException_ShouldBeThrew()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper)
            {
                Model = new CreateGenreViewModel()
                {
                    Name = "Action"
                }
            };

            FluentActions.Invoking(() =>
            {
                var result = command.Handle();

            }).Should().Throw<InvalidOperationException>().And.Message.Should().Be(Messages.GenreAlreadyExists);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper)
            {
                Model = new CreateGenreViewModel()
                {
                    Name = StringHelper.GenerateString(20)
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Invoke();

            _context.Genres.ToList().Find(x => x.Name == command.Model.Name).Should().NotBeNull();
        }

    }
}
