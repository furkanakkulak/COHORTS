using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.GenreOperation.Update
{
    public class UpdateGenreCommand
    {
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateGenreViewModel Model { get; set; } = new UpdateGenreViewModel();

        public UpdateGenreCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result Handle()
        {
            Genre updatedGenre = CheckIfGenreExists();

            updatedGenre.Name = Model.Name != default ? Model.Name : updatedGenre.Name;

            _dbContext.SaveChanges();

            return new SuccessResult(message: Messages.GenreSuccessfullyUpdated);
        }

        private Genre CheckIfGenreExists()
        {
            Genre? searchedGenre = _dbContext.Genres
                .SingleOrDefault(x => x.Id == Model.Id);

            if (searchedGenre == null)
                throw new EntityNullException(Messages.GenreDoesNotExist);

            return searchedGenre;
        }
    }

    public class UpdateGenreViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
