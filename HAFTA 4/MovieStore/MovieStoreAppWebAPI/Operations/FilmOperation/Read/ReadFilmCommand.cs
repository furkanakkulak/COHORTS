using AutoMapper;

using Microsoft.EntityFrameworkCore;

using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Extensions;
using MovieStoreAppWebAPI.Extensions.Repository;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Read;
using MovieStoreAppWebAPI.Operations.GenreOperation.Read;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Read;
using MovieStoreAppWebAPI.RequestFeatures;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.FilmOperation.Read
{
    public class ReadFilmCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public FilmParameters Parameters { get; set; } = new FilmParameters();
        public ReadFilmViewModel Model { get; set; } = new ReadFilmViewModel();
        public ReadFilmCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DataResult<List<ReadFilmViewModel>> GetAll()
        {
            List<Film> films = _context.Films
                .Include(x=>x.Director)
                .Include(x=>x.Genre)
                .Include(x => x.Players)
                .AsQueryable()
                .FilterByName(Parameters)
                .FilterByGenreId(Parameters)
                .FilterByDirectorId(Parameters)
                .FilterByPlayerIds(Parameters)
                .FilterByPublishedDate(Parameters)
                .ToList();

            List<ReadFilmViewModel> viewModels = _mapper.Map<List<ReadFilmViewModel>>(films);

            return new SuccessDataResult<List<ReadFilmViewModel>>(data : viewModels);
        }

        public DataResult<ReadFilmViewModel> GetById()
        {
            Film? searchedFilm = _context.Films
                .Include(x => x.Director)
                .Include(x => x.Genre)
                .Include(x => x.Players)
                .SingleOrDefault(x => x.Id == Model.Id);

            ReadFilmViewModel viewModel = _mapper.Map<ReadFilmViewModel>(searchedFilm);

            return new SuccessDataResult<ReadFilmViewModel>(data: viewModel);
        }
    }

    public class ReadFilmViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime PublishedDate { get; set; }
        public decimal Price { get; set; }
        public string? About { get; set; }
        public ReadGenreViewModel Genre { get; set; } = new ReadGenreViewModel();
        public ReadDirectorViewModel Director { get; set; } = new ReadDirectorViewModel();
        public List<ReadPlayerViewModel> Players { get; set; } = new List<ReadPlayerViewModel>();
    }

}
