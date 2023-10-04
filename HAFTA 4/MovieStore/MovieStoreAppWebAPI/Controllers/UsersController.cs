using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MovieStoreAppWebAPI.ActionFilters;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.UserOperation.Delete;
using MovieStoreAppWebAPI.Operations.UserOperation.Read;
using MovieStoreAppWebAPI.RequestFeatures;

using System.Security.Claims;

namespace MovieStoreAppWebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersController(IMovieStoreDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        [Authorize(Roles = "Admin")]
        [Validate(typeof(DeleteUserViewModelValidator))]
        [HttpDelete]
        public IActionResult DeleteUser([FromBody]DeleteUserViewModel viewModel)
        {
            DeleteUserCommand command = new DeleteUserCommand(_dbContext)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetUsers([FromQuery]UserParameters parameters)
        {
            ReadUserCommand command = new ReadUserCommand(_dbContext, _mapper)
            {
                Parameters = parameters
            };

            return Ok(command.GetAll());
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("getUser")]
        public IActionResult GetUser()
        {
            string usernameClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

            ReadUserCommand command = new ReadUserCommand(_dbContext, _mapper)
            {
                Model = new ReadUserViewModel()
                {
                    Username = usernameClaim
                }
            };

            return Ok(command.GetByUsername());
        }

    }
}
