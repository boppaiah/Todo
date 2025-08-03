namespace TodoAPI.DTOs
{
    public record UpdateTodoRequest(
        Guid Id,
        string Name,
        string Description,
        int Priority,
        bool IsSelected,
        DateTime StartDate,
        DateTime EndDate

        );
}
