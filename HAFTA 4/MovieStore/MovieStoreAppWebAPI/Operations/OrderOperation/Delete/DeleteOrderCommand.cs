using AutoMapper;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.OrderOperation.Delete
{
    public class DeleteOrderCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public DeleteOrderViewModel Model { get; set; } = new DeleteOrderViewModel();
        public DeleteOrderCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result Handle()
        {
            Order searchedOrder = CheckIfOrderExists();

            searchedOrder.IsDeleted = true;
            _dbContext.Orders.Update(searchedOrder);

            _dbContext.SaveChanges();

            return new SuccessResult(message: Messages.OrderSuccessfullyDeleted);
        }


        private Order CheckIfOrderExists()
        {
            Order? searchedOrder = _dbContext.Orders.SingleOrDefault(x => x.Id == Model.Id);

            if (searchedOrder == null)
                throw new EntityNullException(Messages.OrderDoesNotExist);

            return searchedOrder;
        }

    }

    public class DeleteOrderViewModel
    {
        public int Id { get; set; }
    }
}
