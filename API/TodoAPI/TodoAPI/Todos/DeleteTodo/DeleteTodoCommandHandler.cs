using TodoAPI.Data;
using TodoAPI.Shared.CQRS;
using TodoAPI.Shared.Exceptions;

namespace TodoAPI.Todos.DeleteTodo
{
    public record DeleteTodoCommand(Guid Id):ICommand<DeleteTodoResponse>;

    public record DeleteTodoResponse(bool IsSuccess);
    public class DeleteTodoCommandHandler : ICommandHandler<DeleteTodoCommand, DeleteTodoResponse>
    {
        private readonly TodoDb _context;

        public DeleteTodoCommandHandler(TodoDb context)
        {
            _context = context;
        }
        public async Task<DeleteTodoResponse> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todoItem = _context.Todos
                .Where(t => t.Id == request.Id)
                .FirstOrDefault();

            if(todoItem == null)
            {
                throw new NotFoundException($"Todo Item not found for ItemId:{request.Id}");
            }

            _context.Todos.Remove(todoItem);
            await _context.SaveChangesAsync();

            return new DeleteTodoResponse(true);
        }
    }
}
