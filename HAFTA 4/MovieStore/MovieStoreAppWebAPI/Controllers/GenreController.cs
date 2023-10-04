using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using MovieStoreAppWebAPI.ActionFilters;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Create;
using MovieStoreAppWebAPI.Operations.GenreOperation.Create;
using MovieStoreAppWebAPI.Operations.GenreOperation.Delete;
using MovieStoreAppWebAPI.Operations.GenreOperation.Read;
using MovieStoreAppWebAPI.Operations.GenreOperation.Update;
using MovieStoreAppWebAPI.RequestFeatures;

namespace MovieStoreAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        [Validate(typeof(DeleteGenreViewModelValidator))]
        [HttpDelete]
        public IActionResult Delete([FromBody]DeleteGenreViewModel viewModel)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery]GenreParameters parameters)
        {
            ReadGenreCommand command = new ReadGenreCommand(_context, _mapper)
            {
                Parameters = parameters
            };

            return Ok(command.GetAll());
        }

        [Validate(typeof(ReadGenreViewModelValidator))]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ReadGenreCommand command = new ReadGenreCommand(_context, _mapper)
            {
                Model = new ReadGenreViewModel()
                {
                    Id = id
                }
            };

            return Ok(command.GetById());
        }

        [Validate(typeof(CreateGenreViewModelValidator))]
        [HttpPost]
        public IActionResult Add([FromBody] CreateGenreViewModel viewModel)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }


        [Validate(typeof(UpdateGenreViewModelValidator))]
        [HttpPut]
        public IActionResult Update([FromBody] UpdateGenreViewModel viewModel)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context)
            {
                Model = viewModel,
            };

            return Ok(command.Handle());
        }
    }
}
