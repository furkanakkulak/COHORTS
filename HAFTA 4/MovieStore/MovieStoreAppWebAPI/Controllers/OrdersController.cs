using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MovieStoreAppWebAPI.ActionFilters;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Operations.OrderOperation.Create;
using MovieStoreAppWebAPI.Operations.OrderOperation.Delete;
using MovieStoreAppWebAPI.Operations.OrderOperation.Read;
using MovieStoreAppWebAPI.Operations.UserOperation.Read;
using MovieStoreAppWebAPI.RequestFeatures;

using System.Security.Claims;

namespace MovieStoreAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrdersController(IMovieStoreDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll([FromQuery] OrderParameters parameters)
        {
            ReadOrderCommand command = new ReadOrderCommand(_dbContext, _mapper)
            {
                Parameters = parameters
            };

            return Ok(command.GetAll());
        }

        [Validate(typeof(ReadOrderViewModelValidator))]
        [Authorize(Roles = "User")]
        [HttpGet("getAllByUser")]
        public IActionResult GetAllByUser([FromQuery] OrderBaseParameters parameters)
        {
            string usernameClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

            ReadOrderCommand command = new ReadOrderCommand(_dbContext, _mapper)
            {
                Parameters = parameters,
                Model = new ReadOrderViewModel()
                {
                    User = new ReadUserViewModel()
                    {
                        Username = usernameClaim
                    }
                }
            };

            return Ok(command.GetAll());
        }

        [Authorize(Roles = "Customer")]
        [Validate(typeof(CreateOrderViewModelValidator))]
        [HttpPost("buyFilm")]
        public IActionResult BuyFilm([FromBody]CreateOrderViewModel viewModel)
        {
            string usernameClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name); 
            viewModel.UserUsername = usernameClaim;

            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }

        [Authorize(Roles = "Admin")]
        [Validate(typeof(DeleteOrderViewModelValidator))]
        [HttpDelete("deleteOrder")]
        public IActionResult DeleteOrder([FromBody]DeleteOrderViewModel viewModel)
        {
            DeleteOrderCommand command = new DeleteOrderCommand(_dbContext)
            {
                Model = viewModel
            };

            return Ok(command.Handle());
        }
    }
}
