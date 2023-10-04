using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using MovieStoreAppWebAPI.ActionFilters;
using MovieStoreAppWebAPI.Operations.AuthOperation.Login;
using MovieStoreAppWebAPI.Operations.AuthOperation.RefreshToken;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.UserOperation.Create;

namespace MovieStoreAppWebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(IMovieStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        [Validate(typeof(UserLoginViewModelValidator))]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginViewModel viewModel)
        {
            UserLoginCommand command = new UserLoginCommand(_dbContext,_configuration)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }

        [Validate(typeof(TokenModelValidator))]
        [HttpPost("refreshToken")]
        public IActionResult RefreshToken([FromBody] TokenModel viewModel)
        {
            UserRefreshCommand command = new UserRefreshCommand(_dbContext, _configuration)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }

        [Validate(typeof(CreateUserViewModelValidator))]
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] CreateUserViewModel viewModel)
        {
            CreateUserCommand command = new CreateUserCommand(_dbContext, _mapper)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }
    }
}
