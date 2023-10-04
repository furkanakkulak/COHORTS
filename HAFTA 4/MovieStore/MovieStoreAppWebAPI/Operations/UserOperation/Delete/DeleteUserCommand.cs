using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.UserOperation.Delete
{
    public class DeleteUserCommand
    {
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteUserViewModel Model { get; set; } = new DeleteUserViewModel();
        public DeleteUserCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result Handle()
        {
            User searchedUser = CheckIfUserExists();

            _dbContext.Users.Remove(searchedUser);
            _dbContext.SaveChanges();

            return new SuccessResult(message: Messages.UserSuccessfullyDeleted);
        }

        private User CheckIfUserExists()
        {
            User? searchedUser = _dbContext.Users.SingleOrDefault(x => x.Id == Model.Id);

            if (searchedUser == null)
                throw new EntityNullException(Messages.UserDoesNotExist);

            return searchedUser;
        }
    }

    public class DeleteUserViewModel
    {
        public int Id { get; set; }
    }
}
