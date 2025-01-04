using Tosk.TodoTask.Models;

namespace Tosk.TodoTask.Services;

public interface ITodoService
{
    Task AddAsync(string task);
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<IEnumerable<Todo>> GetAllCompletedAsync();
    Task<IEnumerable<Todo>> GetAllImportantAsync();
    Task ToggleCompletionAsync(Todo todo);
    Task ToggleImportanceAsync(Todo todo);
    Task DeleteAsync(Todo todo);
}
