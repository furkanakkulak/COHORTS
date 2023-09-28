using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using webapi.Applications.GenreOperations.Queries.GetGenres;
using webapi.DBOperations;

namespace webapi.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetGenreDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(
                genre => genre.isActive && genre.Id == GenreId
            );
            if (genre is null)
                throw new InvalidOperationException("Genre is not found!");

            GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(genre);
            return vm;
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
