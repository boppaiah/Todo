using TodoAPI;
using TodoAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationRegistrations(builder.Configuration);

var app = builder.Build();
//seed the db
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoDb>();
    DbInitializer.Seed(dbContext);
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// Configure the HTTP request pipeline.
app.UseCors("TodoAngularClient");
app.UseHttpsRedirection();
app.UseExceptionHandler(opts => { });

app.MapControllers();

app.Run();
