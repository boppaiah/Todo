using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.Metadata;
using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Todos.CreateTodo;

namespace TodoAPI.Tests
{
    public class CreateTodoCommandHandlerTests
    {
        private readonly CreateTodoCommandValidator _createTodoCommandValidator;

        public CreateTodoCommandHandlerTests() {

            _createTodoCommandValidator = new CreateTodoCommandValidator();
        }

        [Fact]
        public async Task Handle_Should_CreateTodo_WithCorrectDescription()
        {
            //arrange
            var request = new CreateTodoCommand("Test", "Test this descrition", false, DateTime.Now, DateTime.Now, 1);

            var options = new DbContextOptionsBuilder<TodoDb>()
                .UseInMemoryDatabase("TestDb-Create")
                .Options;


            //act
            using (var context = new TodoDb(options))
            {
                var handler = new CreateTodoCommandHandler(context);

                // Act
                var result = await handler.Handle(request, CancellationToken.None);

                // Assert
                var todos = await context.Todos.ToListAsync();
                Assert.Single(todos);
                Assert.Equal(request.Description, todos[0].Description);
            }

        }

        [Fact]
        public async Task Should_ThrowValidationError_When_Name_Is_Empty()
        {
            //arrange
            var command = new CreateTodoCommand("", "Test this descrition", false, DateTime.Now, DateTime.Now, 1);

            //act
            var result = await _createTodoCommandValidator.ValidateAsync(command);

            //assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "Name" && x.ErrorMessage == "Todo name is required.");
        }
    }
}