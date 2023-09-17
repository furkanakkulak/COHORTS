using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly FakeAuthenticationService _authService;

        public UserController(FakeAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                // Authenticate the user
                bool isAuthenticated = _authService.Authenticate(model.Username, model.Password);

                if (isAuthenticated)
                {
                    return Ok("Login successful");
                }
            }

            return Unauthorized("Login failed");
        }
    }
}
