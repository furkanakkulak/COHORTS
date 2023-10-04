using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.FilmOperation.Delete
{
    public class DeleteFilmCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public DeleteFilmViewModel Model { get; set; } = new DeleteFilmViewModel();

        public DeleteFilmCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result Handle()
        {
            Film searchedFilm = CheckIfFilmExists();

            _dbContext.Films.Remove(searchedFilm);
            _dbContext.SaveChanges();

            return new SuccessResult(message: Messages.FilmSuccessfullyDeleted);
        }

        private Film CheckIfFilmExists()
        {
            Film? searchedFilm = _dbContext.Films.SingleOrDefault(x => x.Id == Model.Id);

            if (searchedFilm == null)
                throw new EntityNullException(Messages.FilmDoesNotExist);

            return searchedFilm;
        }
    }

    public class DeleteFilmViewModel
    {
        public int Id { get; set; }
    }
}
