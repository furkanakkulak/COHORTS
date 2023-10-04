using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MovieStoreAppWebAPI.ActionFilters;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.GenreOperation.Update;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Create;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Delete;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Read;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Update;
using MovieStoreAppWebAPI.RequestFeatures;

namespace MovieStoreAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public PlayersController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PlayerParameters parameters)
        {
            ReadPlayerCommand command = new ReadPlayerCommand(_context,_mapper)
            {
                Parameters = parameters
            };

            return Ok(command.GetAll());
        }

        [Validate(typeof(ReadPlayerViewModelValidator))]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ReadPlayerCommand command = new ReadPlayerCommand(_context, _mapper)
            {
                Model = new ReadPlayerViewModel()
                {
                    Id = id
                }
            };

            return Ok(command.GetById());
        }

        [Authorize(Roles = "Admin")]
        [Validate(typeof(DeletePlayerViewModelValidator))]
        [HttpDelete]
        public IActionResult Delete([FromBody]DeletePlayerViewModel viewModel)
        {
            DeletePlayerCommand command = new DeletePlayerCommand(_context)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }

        [Authorize(Roles = "Admin")]
        [Validate(typeof(CreatePlayerViewModelValidator))]
        [HttpPost]
        public IActionResult Add([FromBody] CreatePlayerViewModel viewModel)
        {
            CreatePlayerCommand command = new CreatePlayerCommand(_context, _mapper)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }

        [Authorize(Roles = "Admin")]
        [Validate(typeof(UpdatePlayerViewModelValidator))]
        [HttpPut]
        public IActionResult Update([FromBody] UpdatePlayerViewModel viewModel)
        {
            UpdatePlayerCommand command = new UpdatePlayerCommand(_context)
            {
                Model = viewModel,
            };

            return Ok(command.Handle());
        }
    }
}
