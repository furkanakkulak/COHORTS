using AutoMapper;

using Microsoft.EntityFrameworkCore;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Utilities.Results;

using System.IO;

namespace MovieStoreAppWebAPI.Operations.FilmOperation.Update
{
    public class UpdateFilmCommand
    {
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateFilmViewModel Model { get; set; } = new UpdateFilmViewModel();

        public UpdateFilmCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result Handle()
        {
            Film updatedFilm = CheckIfFilmExists();
            Genre genre = CheckIfGenreExists();
            Director director = CheckIfDirectorExists();
            List<Player> players = CheckIfPlayerExists();

            updatedFilm.Price = Model.Price != default ? Model.Price : updatedFilm.Price;
            updatedFilm.PublishedDate = Model.PublishedDate != default ? Model.PublishedDate : updatedFilm.PublishedDate;
            updatedFilm.Name = Model.Name != default ? Model.Name : updatedFilm.Name;
            updatedFilm.About = Model.About != default ? Model.About : updatedFilm.About;

            updatedFilm.Genre = genre != default ? genre : updatedFilm.Genre;
            updatedFilm.Director = director != default ? director : updatedFilm.Director;
            updatedFilm.Players = players != default ? players : updatedFilm.Players;

            _dbContext.SaveChanges();

            return new SuccessResult(message: Messages.FilmSuccessfullyUpdated);
        }

        private Film CheckIfFilmExists()
        {
            Film? searchedFilm = _dbContext.Films
                .Include(x => x.Director)
                .Include(x => x.Genre)
                .Include(x => x.Players)
                .SingleOrDefault(x => x.Id == Model.Id);

            if (searchedFilm == null)
                throw new EntityNullException(Messages.FilmDoesNotExist);

            return searchedFilm;
        }

        private List<Player> CheckIfPlayerExists()
        {
            List<Player> players = new List<Player>();

            foreach (int playerId in Model.PlayerIds)
            {
                Player? searchedPlayer = _dbContext.Players.SingleOrDefault(x => x.Id == playerId);

                if (searchedPlayer == null)
                    throw new EntityNullException(Messages.PlayerDoesNotExist);

                players.Add(searchedPlayer);
            }

            return players;
        }
        private Director CheckIfDirectorExists()
        {
            Director? searchedDirector = _dbContext.Directors.SingleOrDefault(x => x.Id == Model.DirectorId);

            if (searchedDirector == null)
                throw new EntityNullException(Messages.DirectorDoesNotExist);

            return searchedDirector;
        }
        private Genre CheckIfGenreExists()
        {
            Genre? searchedGenre = _dbContext.Genres.SingleOrDefault(x => x.Id == Model.GenreId);

            if (searchedGenre == null)
                throw new EntityNullException(Messages.GenreDoesNotExist);

            return searchedGenre;
        }
    }

    public class UpdateFilmViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime PublishedDate { get; set; }
        public decimal Price { get; set; }
        public string? About { get; set; }
        public List<int> PlayerIds { get; set; } = new List<int>();
        public int DirectorId { get; set; }
        public int GenreId { get; set; }
    }
}
