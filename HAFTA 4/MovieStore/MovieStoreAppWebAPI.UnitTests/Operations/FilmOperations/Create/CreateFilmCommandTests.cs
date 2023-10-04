using AutoMapper;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Create;
using MovieStoreAppWebAPI.Operations.FilmOperation.Create;
using MovieStoreAppWebAPI.UnitTests.Helpers;
using MovieStoreAppWebAPI.UnitTests.TestSetup;

namespace MovieStoreAppWebAPI.UnitTests.Operations.DirectorOperations.Create
{
    public class CreateFilmCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateFilmCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenSavedDirectorNameGiven_InvalidOperationException_ShouldBeThrew()
        {
            CreateFilmCommand command = new CreateFilmCommand(_context,_mapper)
            {
                Model = new CreateFilmViewModel()
                {
                    Name = "Interstellar",
                    DirectorId = 1,
                    GenreId = 1,
                    Price = 200,
                    PublishedDate = new DateTime(2015, 8, 26),
                    About = "When Earth becomes uninhabitable in the future, a farmer and ex-NASA pilot, Joseph Cooper, is tasked to pilot a spacecraft, along with a team of researchers, to find a new planet for humans.",
                    PlayerIds = new List<int>() { 1,2}
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
            CreateFilmCommand command = new CreateFilmCommand(_context, _mapper)
            {
                Model = new CreateFilmViewModel()
                {
                    Name = StringHelper.GenerateString(20),
                    DirectorId = 1,
                    GenreId = 1,
                    Price = 200,
                    PublishedDate = new DateTime(2015, 8, 26),
                    About = "When Earth becomes uninhabitable in the future, a farmer and ex-NASA pilot, Joseph Cooper, is tasked to pilot a spacecraft, along with a team of researchers, to find a new planet for humans.",
                    PlayerIds = new List<int>() { 1, 2 }
                }
            };

            FluentActions.Invoking(() =>
            {
                command.Handle();
            }).Invoke();

            Film searchedFilm = _context.Films
                .Include(x => x.Director)
                .Include(x => x.Genre)
                .Include(x => x.Players)
                .ToList().Find(x => x.Name == command.Model.Name);
            searchedFilm.Should().NotBeNull();
            searchedFilm.Name.Should().Be(command.Model.Name);
            searchedFilm.Price.Should().Be(command.Model.Price);
            searchedFilm.About.Should().Be(command.Model.About);
            searchedFilm.PublishedDate.Should().Be(command.Model.PublishedDate);
            searchedFilm.Director.Id.Should().Be(command.Model.DirectorId);
            searchedFilm.Genre.Id.Should().Be(command.Model.GenreId);
            
            searchedFilm.Players.Count.Should().Be(command.Model.PlayerIds.Count);
        }

    }
}
