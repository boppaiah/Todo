using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.Metadata;
using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Todos.CreateTodo;
using TodoAPI.Todos.UpdateTodo;

namespace TodoAPI.Tests
{
    public class UpdateTodoCommandHandlerTests
    {

        [Fact]
        public async Task Handle_Should_UpdateTodo_WithCorrectDescription()
        {
            //arrange
            var request = new UpdateTodoCommand(Guid.Parse("11111111-1111-1111-1111-111111111111"),
                "Updated Test", 
                "Updated Test this descrition", 
                false, 
                DateTime.Now, 
                DateTime.Now, 
                1);

            var options = new DbContextOptionsBuilder<TodoDb>()
                .UseInMemoryDatabase("TestDb-Update")
                .Options;

            using (var context = new TodoDb(options))
            {
                context.Todos.AddRange(
                    new TodoItem()
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        Name = "Test-1",
                        Description = "This is a description 1",
                        EndDate = DateTime.Now,
                        StartDate = DateTime.Now,
                        IsSelected = false,
                        Priority = 1
                    },
                    new TodoItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test-2",
                        Description = "This is a description 2",
                        EndDate = DateTime.Now,
                        StartDate = DateTime.Now,
                        IsSelected = false,
                        Priority = 1
                    }
                );

                await context.SaveChangesAsync();

            }

            //act
            using (var context = new TodoDb(options))
            {
                var handler = new UpdateTodoCommandHandler(context);

                // Act
                var result = await handler.Handle(request, CancellationToken.None);

                // Assert
                var todos = await context.Todos.ToListAsync();
                Assert.Equal(request.Description, todos[0].Description);
            }

        }
    }
}