using AutoMapper;

using Microsoft.EntityFrameworkCore;
using MovieStoreAppWebAPI.Extensions.Repository;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.FilmOperation.Read;
using MovieStoreAppWebAPI.Operations.UserOperation.Read;
using MovieStoreAppWebAPI.RequestFeatures;
using MovieStoreAppWebAPI.Utilities.Results;

namespace MovieStoreAppWebAPI.Operations.OrderOperation.Read
{
    public class ReadOrderCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public ReadOrderViewModel Model { get; set; } = new ReadOrderViewModel();
        public OrderBaseParameters Parameters { get; set; } = new OrderBaseParameters();
        public ReadOrderCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public DataResult<List<ReadOrderViewModel>> GetAll()
        {
            var customerOrders = _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Film)
                        .ThenInclude(f => f.Genre)
                    .Include(o => o.Film)
                        .ThenInclude(f => f.Director)
                    .Include(o => o.Film)
                        .ThenInclude(f => f.Players)
                    .AsQueryable()
                    .FilterByUserId(Model.User.Id)
                    .FilterByFilmName(Parameters)
                    .FilterByOrderedDate(Parameters)
                    .ToList();

            List<ReadOrderViewModel> viewModels = _mapper.Map<List<ReadOrderViewModel>>(customerOrders);

            return new SuccessDataResult<List<ReadOrderViewModel>>(data: viewModels);
        }

        public DataResult<List<ReadOrderViewModel>> GetAllByUserUsername()
        {
            var customerOrders = _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Film)
                        .ThenInclude(f => f.Genre)
                    .Include(o => o.Film)
                        .ThenInclude(f => f.Director)
                    .Include(o => o.Film)
                        .ThenInclude(f => f.Players)
                    .AsQueryable()
                    .FilterByFilmName(Parameters)
                    .FilterByOrderedDate(Parameters)
                    .Where(x => x.User.Username == Model.User.Username)
                    .ToList();

            List<ReadOrderViewModel> viewModels = _mapper.Map<List<ReadOrderViewModel>>(customerOrders);

            return new SuccessDataResult<List<ReadOrderViewModel>>(data: viewModels);
        }


    }

    public class ReadOrderViewModel
    {
        public int Id { get; set; }
        public ReadFilmViewModel Film { get; set; } = new ReadFilmViewModel();
        public DateTime OrderedDate { get; set; }
        public ReadUserViewModel User { get; set; } = new ReadUserViewModel();
    }


}
