using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Extensions.Repository;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.RequestFeatures;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.GenreOperation.Read
{
    public class ReadGenreCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public ReadGenreViewModel Model { get; set; } = new ReadGenreViewModel();
        public GenreParameters Parameters { get; set; } = new GenreParameters();
        public ReadGenreCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DataResult<List<ReadGenreViewModel>> GetAll()
        {
            List<Genre> genres = _context.Genres
                .AsQueryable()
                .FilterByName(Parameters)
                .ToList();

            List<ReadGenreViewModel> viewModels = _mapper.Map<List<ReadGenreViewModel>>(genres);

            return new SuccessDataResult<List<ReadGenreViewModel>>(data : viewModels);
        }


        public DataResult<ReadGenreViewModel> GetById()
        {
            Genre? searchedGenre = _context.Genres
                .SingleOrDefault(x => x.Id == Model.Id);

            return new SuccessDataResult<ReadGenreViewModel>(data: _mapper.Map<ReadGenreViewModel>(searchedGenre));
        }
    }

    public class ReadGenreViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
