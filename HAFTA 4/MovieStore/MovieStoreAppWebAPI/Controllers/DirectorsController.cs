using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MovieStoreAppWebAPI.ActionFilters;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Create;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Delete;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Read;

using MovieStoreAppWebAPI.Operations.DirectorOperation.Update;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Create;
using MovieStoreAppWebAPI.RequestFeatures;

namespace MovieStoreAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public DirectorsController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery]DirectorParameters parameters)
        {
            ReadDirectorCommand command = new ReadDirectorCommand(_context, _mapper)
            {
                Parameters = parameters
            };

            return Ok(command.GetAll());
        }

        [Validate(typeof(ReadDirectorViewModelValidator))]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ReadDirectorCommand command = new ReadDirectorCommand(_context, _mapper)
            {
                Model = new ReadDirectorViewModel()
                {
                    Id = id
                }
            };

            return Ok(command.GetById());
        }

        [Authorize(Roles = "Admin")]
        [Validate(typeof(CreateDirectorViewModelValidator))]
        [HttpPost]
        public IActionResult Add([FromBody] CreateDirectorViewModel viewModel)
        {
            CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }

        [Authorize(Roles = "Admin")]
        [Validate(typeof(DeleteDirectorViewModelValidator))]
        [HttpDelete]
        public IActionResult Delete([FromQuery] DeleteDirectorViewModel viewModel)
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }

        [Authorize(Roles = "Admin")]
        [Validate(typeof(UpdateDirectorViewModelValidator))]
        [HttpPut]
        public IActionResult Update([FromBody] UpdateDirectorViewModel viewModel)
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context)
            {
                Model = viewModel,
            };

            return Ok(command.Handle());
        }

    }
}
