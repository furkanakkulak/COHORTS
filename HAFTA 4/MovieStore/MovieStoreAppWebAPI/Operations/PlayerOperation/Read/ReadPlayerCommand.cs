using AutoMapper;

using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Extensions;
using MovieStoreAppWebAPI.Extensions.Repository;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.RequestFeatures;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.PlayerOperation.Read
{
    public class ReadPlayerCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public ReadPlayerViewModel Model { get; set; } = new ReadPlayerViewModel();
        public PlayerParameters Parameters { get; set; } = new PlayerParameters();
        public ReadPlayerCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DataResult<List<ReadPlayerViewModel>> GetAll()
        {
            List<Player> players = _context.Players
                .AsQueryable()
                .FilterByNameSurname(Parameters)
                .ToList();

            List<ReadPlayerViewModel> viewModels = _mapper.Map<List<ReadPlayerViewModel>>(players);

            return new SuccessDataResult<List<ReadPlayerViewModel>>(data : viewModels);
        }
    
        public DataResult<ReadPlayerViewModel> GetById()
        {
            Player? searchedPlayer = _context.Players.SingleOrDefault(x => x.Id == Model.Id);

            var viewModel = _mapper.Map<ReadPlayerViewModel>(searchedPlayer);

            return new SuccessDataResult<ReadPlayerViewModel>(data: viewModel);
        }
    }

    public class ReadPlayerViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
