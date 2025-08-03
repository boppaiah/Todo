using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Shared.CQRS;
using TodoAPI.Shared.Exceptions;


namespace TodoAPI.Todos.GetTodoByPriority
{
    public record GetTodoByPriorityQuery(int Priority):IQuery<GetTodoByPriorityResponse>;
    public record GetTodoByPriorityResponse(IEnumerable<TodoItem> TodoItems);
    public class GetTodoByPriorityQueryHandler : IQueryHandler<GetTodoByPriorityQuery, GetTodoByPriorityResponse>
    {
        private readonly TodoDb _context;

        public GetTodoByPriorityQueryHandler(TodoDb context)
        {
            _context = context;
        }
        public async Task<GetTodoByPriorityResponse> Handle(GetTodoByPriorityQuery request, CancellationToken cancellationToken)
        {
            var priorityItems = await  _context.Todos
                .Where(t => t.Priority == request.Priority)
                .ToListAsync();

            if (!priorityItems.Any())
            {
                throw new NotFoundException("No item with this priority is found");
            }


            return new GetTodoByPriorityResponse(priorityItems);
        }
    }
}
