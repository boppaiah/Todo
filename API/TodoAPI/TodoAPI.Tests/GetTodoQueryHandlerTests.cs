using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.Metadata;
using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Todos.CreateTodo;
using TodoAPI.Todos.GetTodoById;
using TodoAPI.Todos.GetTodos;

namespace TodoAPI.Tests
{
    public class GetTodoQueryHandlerTests
    {

        [Fact]
        public async Task Handle_Should_ReturnTwoRows()
        {
            //arrange
            var request = new GetTodosQuery(1, 5);

            var options = new DbContextOptionsBuilder<TodoDb>()
                .UseInMemoryDatabase("TestDb-Get")
                .Options;

            using (var context = new TodoDb(options))
            {
                context.Todos.AddRange(
                    new TodoItem()
                    {
                        Id = Guid.NewGuid(),
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

            using (var context = new TodoDb(options))
            {
                var handler = new GetTodosQueryHandler(context);
                // Act
                var result = await handler.Handle(request, CancellationToken.None);

                // Assert
                var todos = await context.Todos.ToListAsync();
                Assert.Equal(2, todos.Count);

            }

        }
    }
}