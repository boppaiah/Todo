using TodoAPI.Shared.CQRS;
using TodoAPI.Shared.Exceptions;
using TodoAPI.Data;
using TodoAPI.Models;


namespace TodoAPI.Todos.GetTodoById
{
    public record GetTodoByIdQuery(Guid Id):IQuery<GetTodoByIdQueryResponse>;

    public record GetTodoByIdQueryResponse(TodoItem TodoItem);

    public class GetTodoByIdQueryHandler : IQueryHandler<GetTodoByIdQuery, GetTodoByIdQueryResponse>
    {
        private readonly TodoDb _context;

        public GetTodoByIdQueryHandler(TodoDb context)
        {
            _context = context;
        }
        public async Task<GetTodoByIdQueryResponse> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            var todoItem = _context.Todos
                .Where(t => t.Id == request.Id)
                .SingleOrDefault();

            if (todoItem == null)
            {
                throw new NotFoundException($"Todo Item not found for ItemId:{request.Id}");
            }

            return new GetTodoByIdQueryResponse(todoItem);

        }
    }
}
