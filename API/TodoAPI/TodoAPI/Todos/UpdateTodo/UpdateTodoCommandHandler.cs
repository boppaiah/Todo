using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Shared.CQRS;
using TodoAPI.Shared.Exceptions;

namespace TodoAPI.Todos.UpdateTodo
{
    public record UpdateTodoCommand(Guid Id
        , string Name
        , string Description
        , bool IsSelected
        , DateTime StartDate
        , DateTime EndDate
        , int Priority) :ICommand<UpdateTodoResponse>;
    public record UpdateTodoResponse(bool IsSuccess);
    public class UpdateTodoCommandHandler : ICommandHandler<UpdateTodoCommand, UpdateTodoResponse>
    {

        private readonly TodoDb _context;

        public UpdateTodoCommandHandler(TodoDb context)
        {
            _context = context;
        }

        public async Task<UpdateTodoResponse> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todoItem = _context.Todos
                           .Where(t => t.Id == request.Id)
                           .SingleOrDefault();

            if (todoItem == null)
            {
                throw new NotFoundException($"Todo Item not found for ItemId:{request.Id}"); 
            }

            //update the todo item 
            todoItem.Name = request.Name;
            todoItem.Description = request.Description;
            todoItem.IsSelected = request.IsSelected;
            todoItem.Priority = request.Priority;
            todoItem.StartDate = request.StartDate;
            todoItem.EndDate = request.EndDate;

            _context.Todos.Update(todoItem);
            await _context.SaveChangesAsync();

            return new UpdateTodoResponse(true);
        }
    }
}
