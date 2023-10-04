using AutoMapper;

using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Extensions.Repository;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.RequestFeatures;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.DirectorOperation.Read
{
    public class ReadDirectorCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public ReadDirectorViewModel Model { get; set; } = new ReadDirectorViewModel();
        public DirectorParameters Parameters { get; set; } = new DirectorParameters();

        public ReadDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DataResult<List<ReadDirectorViewModel>> GetAll()
        {
            List<Director> directors = _context.Directors
                .AsQueryable()
                .FilterByNameSurname(Parameters)
                .ToList();

            List<ReadDirectorViewModel> viewModels = _mapper.Map<List<ReadDirectorViewModel>>(directors);

            return new SuccessDataResult<List<ReadDirectorViewModel>>(data : viewModels);
        }

        public DataResult<ReadDirectorViewModel> GetById()
        {
            Director? searchedDirector = _context.Directors.SingleOrDefault(x => x.Id == Model.Id);

            ReadDirectorViewModel viewModel = _mapper.Map<ReadDirectorViewModel>(searchedDirector);

            return new SuccessDataResult<ReadDirectorViewModel>(data: viewModel);
        }
    }

    public class ReadDirectorViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
