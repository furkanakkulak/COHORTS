using AutoMapper;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Utilities.Results;

using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieStoreAppWebAPI.Operations.FilmOperation.Create
{
    public class CreateFilmCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateFilmViewModel Model { get; set; } = new CreateFilmViewModel();
        public CreateFilmCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Result Handle()
        {
            CheckIfFilmAlreadyExists();
            Genre genre = CheckIfGenreExists();
            Director director = CheckIfDirectorExists();
            List<Player> players = CheckIfPlayerExists();

            Film newFilm = _mapper.Map<Film>(Model);
            newFilm.Players = players;
            newFilm.Genre = genre;
            newFilm.Director = director;

            _dbContext.Films.Add(newFilm);

            _dbContext.SaveChanges();

            return new SuccessResult(message: Messages.FilmSuccessfullyCreated);
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
        private void CheckIfFilmAlreadyExists()
        {
            Film? searchedFilm = _dbContext.Films.SingleOrDefault(x => x.Name == Model.Name);

            if (searchedFilm != null)
                throw new InvalidOperationException(Messages.FilmAlreadyExists);
        }
    }

    public class CreateFilmViewModel
    {
        public string? Name { get; set; }
        public DateTime PublishedDate { get; set; }
        public decimal Price { get; set; }
        public string? About { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public List<int> PlayerIds { get; set; } = new List<int>();
    }
}
