using Authentication.Dao.UnitofWork;
using Authentication.Filters;
using Authentication.Repository;
using Authentication.Repository.GenericRepositories;
using Authentication.Services;

using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Authentication.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITodoItemService,TodoItemService>();
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();

            services.AddScoped<IUnitofWork, UnitofWork>();

            services.AddScoped<Hashtable>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = acttionContext =>
                {
                    var errors = acttionContext.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value?.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();
                    var problemDetails = new ProblemDetails
                    {
                        Status = 400,
                        Title = "Validation failed",
                        Detail = "One or more validation errors occurred.",
                        Instance = acttionContext.HttpContext.Request.Path
                    };

                    if (errors != null && errors.Any())
                    {
                        problemDetails.Extensions["errors"] = errors;
                    }

                    return new BadRequestObjectResult(problemDetails);
                };
            });

            return services;
        }
    }
}
