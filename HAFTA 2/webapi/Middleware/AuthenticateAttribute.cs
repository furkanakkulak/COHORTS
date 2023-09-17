using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using webapi.Services;

namespace webapi.Middleware
{
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authService =
                context.HttpContext.RequestServices.GetRequiredService<FakeAuthenticationService>();
            bool isAuthenticated = authService.Authenticate(
                context.HttpContext.User.Identity.Name,
                context.HttpContext.User.Identity.Name
            );

            if (!isAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
