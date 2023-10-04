using AutoMapper;

using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Extensions.Repository;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.RequestFeatures;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.UserOperation.Read
{
    public class ReadUserCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public ReadUserViewModel Model { get; set; } = new ReadUserViewModel();
        public UserParameters Parameters { get; set; } = new UserParameters();

        public ReadUserCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DataResult<List<ReadUserViewModel>> GetAll()
        {
            List<User> users = _context.Users
                .AsQueryable()
                .FilterByNameSurname(Parameters)
                .FilterByUsername(Parameters)
                .FilterByEmail(Parameters)
                .ToList();

            List<ReadUserViewModel> viewModels = _mapper.Map<List<ReadUserViewModel>>(users);

            return new SuccessDataResult<List<ReadUserViewModel>>(data : viewModels);
        }

        public DataResult<ReadUserViewModel> GetByUsername()
        {
            User? searchedUser = _context.Users.SingleOrDefault(x => x.Username == Model.Username);

            ReadUserViewModel viewModel = _mapper.Map<ReadUserViewModel>(searchedUser);

            return new SuccessDataResult<ReadUserViewModel>(data: viewModel);
        }
    }

    public class ReadUserViewModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
    }
}
