using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TodoAPI.Data;
using TodoAPI.Mappings;
using TodoAPI.Shared.Behaviors;
using TodoAPI.Shared.Exceptions;
using TodoAPI.Todos.CreateTodo;

namespace TodoAPI
{
    public static class ApplicationRegistrations
    {
        public static void AddApplicationRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            //setup cors
            services.AddCors(options =>
            {
                options.AddPolicy("TodoAngularClient", policy =>
                {
                    policy.WithOrigins(configuration.GetSection("Angular:url").Value)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });


            //register ef core
            services.AddDbContext<TodoDb>(options =>
            {
                options.UseInMemoryDatabase("TodoList");
            });

            //add validators
            services.AddValidatorsFromAssemblies(new List<Assembly>()
            {
                typeof(UpdateTodoCommandValidator).Assembly,
            });

            //register mediatr
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(
                        typeof(CreateTodoCommand).Assembly
                    );
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

          

            //register automapper
            services.AddAutoMapper(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            });

            //global exception handler
            services.AddExceptionHandler<GlobalExceptionHandler>();



        }  
    }
}
