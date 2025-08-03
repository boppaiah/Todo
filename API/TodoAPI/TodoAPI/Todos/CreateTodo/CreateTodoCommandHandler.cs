using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Shared.CQRS;

namespace TodoAPI.Todos.CreateTodo
{
    public record CreateTodoCommand(
         string Name 
        ,string Description
        ,bool IsSelected
        ,DateTime StartDate 
        ,DateTime EndDate
        ,int Priority) : ICommand<CreateTodoResponse>;
    public record CreateTodoResponse(bool IsSuccess);

    public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, CreateTodoResponse>
    {
        private readonly TodoDb _context;

        public CreateTodoCommandHandler(TodoDb context)
        {
            _context = context;
        }
        public async Task<CreateTodoResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var newTodoItem = new TodoItem()
            {
                Id = Guid.NewGuid(),
                Description = request.Description,
                Name = request.Name,
                IsSelected = request.IsSelected,
                Priority = request.Priority,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            _context.Todos.Add(newTodoItem);
            await _context.SaveChangesAsync();
            return new CreateTodoResponse(true);

        }
    }
}
