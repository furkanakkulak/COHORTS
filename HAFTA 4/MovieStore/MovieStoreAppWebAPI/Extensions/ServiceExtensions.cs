using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.InMemory;

using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MovieStoreAppWebAPI.Services.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MovieStoreAppWebAPI.Utilities.Swagger;

namespace MovieStoreAppWebAPI.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configures swagger options inside of this method
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

                setup.SchemaFilter<SwaggerSchemaFilter>();
            });

        }
    
        /// <summary>
        /// Configures database options inside of this method
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureDatabase(this IServiceCollection services)
        {
            services.AddDbContext<MovieStoreInMemoryDbContext>(); ;

            var scope = services.BuildServiceProvider().CreateScope();
            DataGenerator.Initialize(scope.ServiceProvider);
        }
    
        /// <summary>
        /// Configures JWT Bearer authentication inside of this method
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureAuthentication(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidAudience = configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    
        /// <summary>
        /// Configures all services (scoped,singleton,transient) inside of this method
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IMovieStoreDbContext, MovieStoreInMemoryDbContext>();
            services.AddSingleton<ILoggerService, ConsoleLogger>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
