using AutoMapper;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.PlayerOperation.Create
{
    public class CreatePlayerCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreatePlayerViewModel Model { get; set; } = new CreatePlayerViewModel();
        public CreatePlayerCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Result Handle()
        {
            CheckIfPlayerAlreadyExists();

            Player newPlayer = _mapper.Map<Player>(Model);

            _dbContext.Players.Add(newPlayer);

            _dbContext.SaveChanges();

            return new SuccessResult(message: Messages.PlayerSuccessfullyCreated);
        }


        private void CheckIfPlayerAlreadyExists()
        {
            Player? searchedPlayer = _dbContext.Players.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);

            if (searchedPlayer != null)
                throw new InvalidOperationException(Messages.PlayerAlreadyExists);
        }

    }

    public class CreatePlayerViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
