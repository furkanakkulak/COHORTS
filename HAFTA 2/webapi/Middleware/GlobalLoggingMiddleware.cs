using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace webapi.Middleware
{
    public class GlobalLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log the incoming request
            Console.WriteLine($"Request received: {context.Request.Method} {context.Request.Path}");

            await _next(context);

            // Log the outgoing response
            Console.WriteLine($"Response sent: {context.Response.StatusCode}");
        }
    }
}
