using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Shared.CQRS;

namespace TodoAPI.Todos.GetTodos
{
    public record GetTodosQuery(int? pageNumber=0, int? pageSize=5):IQuery<GetTodosQueryResponse>;
    public record GetTodosQueryResponse(IEnumerable<TodoItem> TodoItems, int TotalRecords);
    public class GetTodosQueryHandler : IQueryHandler<GetTodosQuery, GetTodosQueryResponse>
    {
        private readonly TodoDb _context;
        public GetTodosQueryHandler(TodoDb context)
        {
            _context = context;
        }

        public async Task<GetTodosQueryResponse> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            var todoItems = await _context.Todos
                .OrderBy(t => t.Id)
                .ThenBy(t => t.StartDate)
                .ToListAsync();
            var totalRecords = todoItems.Count;

            return new GetTodosQueryResponse(todoItems, totalRecords);
        }
    }
}
