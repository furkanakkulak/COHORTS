using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MovieStoreAppWebAPI.ActionFilters;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.FilmOperation.Create;
using MovieStoreAppWebAPI.Operations.FilmOperation.Delete;
using MovieStoreAppWebAPI.Operations.FilmOperation.Read;
using MovieStoreAppWebAPI.Operations.FilmOperation.Update;
using MovieStoreAppWebAPI.RequestFeatures;
using MovieStoreAppWebAPI.Services.Logging;

namespace MovieStoreAppWebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public FilmsController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery]FilmParameters parameters)
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper)
            {
                Parameters = parameters
            };

            return Ok(command.GetAll());
        }

        [Validate(typeof(ReadFilmViewModelValidator))]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ReadFilmCommand command = new ReadFilmCommand(_context, _mapper) 
            { 
                Model = new ReadFilmViewModel()
                {
                    Id = id
                }
            };

            return Ok(command.GetById());
        }

        [Authorize(Roles = "Admin")]
        [Validate(typeof(CreateFilmViewModelValidator))]
        [HttpPost]
        public IActionResult Add([FromBody]CreateFilmViewModel viewModel)
        {
            CreateFilmCommand command = new CreateFilmCommand(_context, _mapper)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }

        [Authorize(Roles = "Admin")]
        [Validate(typeof(DeleteFilmViewModelValidator))]
        [HttpDelete]
        public IActionResult Delete([FromQuery]DeleteFilmViewModel viewModel)
        {
            DeleteFilmCommand command = new DeleteFilmCommand(_context)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }

        [Authorize(Roles = "Admin")]
        [Validate(typeof(UpdateFilmViewModelValidator))]
        [HttpPut]
        public IActionResult Update([FromBody] UpdateFilmViewModel viewModel)
        {
            UpdateFilmCommand command = new UpdateFilmCommand(_context)
            {
                Model = viewModel,
            };

            return Ok(command.Handle());
        }

    }
}
