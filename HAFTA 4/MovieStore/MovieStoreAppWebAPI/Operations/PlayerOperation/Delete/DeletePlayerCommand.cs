using Microsoft.EntityFrameworkCore;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Utilities.Results;

using System.Runtime.Intrinsics.X86;

namespace MovieStoreAppWebAPI.Operations.PlayerOperation.Delete
{
    public class DeletePlayerCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public DeletePlayerViewModel Model { get; set; } = new DeletePlayerViewModel();

        public DeletePlayerCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Result Handle()
        {
            Player searchedPlayer = CheckIfPlayerExists(Model.Id);

            //TODO : Transaction yaparken MovieStoreInMemoryDbContext bağımlıyız. Buranin refactor edilmesi gerekiyor

            if(searchedPlayer.Films.Count == 0)
            {
                _dbContext.Players.Remove(searchedPlayer);

                _dbContext.SaveChanges();

                return new SuccessResult(Messages.PlayerSuccessfullyDeleted);
            }


            var transaction = _dbContext.DatabaseInstance.BeginTransaction();
            try
            {
                RemoveTheActorFromTheFilms(searchedPlayer);

                _dbContext.Players.Remove(searchedPlayer);

                _dbContext.SaveChanges();

                transaction.Commit();

                return new SuccessResult(Messages.PlayerSuccessfullyDeleted);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw new ArgumentException(exception.Message);
            }

        }

        private void RemoveTheActorFromTheFilms(Player entity)
        {
            var filmsWherePlayed = _dbContext.Films.Include(x => x.Players).Where(x => x.Players.SingleOrDefault(x => x.Id == Model.Id) != null);
            foreach (var film in filmsWherePlayed)
            {
                film.Players.Remove(entity);
            }
        }

        private Player CheckIfPlayerExists(int id)
        {
            Player? searhedPlayer = _dbContext.Players.SingleOrDefault(x => x.Id == id);

            if (searhedPlayer == null)
                throw new EntityNullException(Messages.PlayerDoesNotExist);

            return searhedPlayer;
        }
    }

    public class DeletePlayerViewModel
    {
        public int Id { get; set; }
    }
}
