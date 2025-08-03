using TodoAPI.Models;
namespace TodoAPI.Data
{
    public static class DbInitializer
    {
        public static void Seed(TodoDb todoDbContext)
        {
            if (!todoDbContext.Todos.Any())
            {
                todoDbContext.Todos.AddRange(
                        new TodoItem
                        {
                            Id = Guid.NewGuid(),
                            Name = "Task One",
                            Description = "This describes the task",
                            IsSelected = false,
                            Priority = 1,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now
                        }
                    );
                todoDbContext.SaveChanges();
            }
        }
    }
}
