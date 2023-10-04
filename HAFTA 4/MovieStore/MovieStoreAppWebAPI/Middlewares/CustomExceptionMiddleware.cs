using FluentValidation;

using Microsoft.IdentityModel.Tokens;

using MovieStoreAppWebAPI.Exceptions;
using MovieStoreAppWebAPI.Services.Logging;
using MovieStoreAppWebAPI.Utilities.Results;

using Newtonsoft.Json;

using System.Diagnostics;
using System.Net;

namespace MovieStoreAppWebAPI.Middlewares
{
    public class CustomExceptionMiddleware
    {
        public readonly RequestDelegate RequestDelegate;
        private ConsoleLogger _loggerService;

        public CustomExceptionMiddleware(RequestDelegate requestDelegate, ILoggerService loggerService)
        {
            RequestDelegate = requestDelegate;
            _loggerService = (ConsoleLogger)loggerService;
        }

        public async Task Invoke(HttpContext context)
        {

            if (context.Request.Path.ToString().StartsWith("/api"))
            {
                Stopwatch watch = new Stopwatch();

                try
                {
                    watch.Start();

                    string message = $"HTTP {context.Request.Method}  - {context.Request.Path}";
                    _loggerService.Write(message);

                    await RequestDelegate(context);

                    watch.Stop();
                    message = $"[Response] HTTP {context.Request.Method}  - {context.Request.Path} - Status Code : {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds}";
                    _loggerService.Write(message);

                    watch.Reset();
                }
                catch (Exception exception)
                {
                    watch.Stop();
                    await HandleException(context, exception, watch);
                }


            }

        }

        private Task HandleException(HttpContext context, Exception exception, Stopwatch watch)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = HandleStatusCode(exception);

            string loggerResult = $"[Error] Http {context.Request.Method} - {context.Response.StatusCode} - Error Message : {exception.Message} - Elapsed Time : {watch.Elapsed.TotalMilliseconds}";

            var result = JsonConvert.SerializeObject(
                new ErrorResult(
                    message : exception.Message)
                );

            _loggerService.Write(loggerResult);

            return context.Response.WriteAsync(result);

        }

        private int HandleStatusCode(Exception exception)
        {
            switch (exception)
            {
                case SecurityTokenException:
                    return (int)HttpStatusCode.BadRequest;
                case ValidationException:
                    return (int)HttpStatusCode.BadRequest;
                case EntityNullException:
                    return (int)HttpStatusCode.NotFound;
                default:
                    return (int)HttpStatusCode.InternalServerError;
            }
        }
    }

    public class ErrorModel
    {
        public string? Method { get; set; }
        public int StatusCode { get; set; }
        public double ElapsedTime { get; set; }
        public string? Message { get; set; }
    }

    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
