namespace TodoAPI.DTOs
{
    public record CreateTodoRequest(
        string Name,
        string Description,
        int Priority,
        bool IsSelected,
        DateTime StartDate,
        DateTime EndDate

        );
}
