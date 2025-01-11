using Tosk.Commons.Enums;
using Tosk.TodoTask.Models;
using Tosk.TodoTask.Repositories;

namespace Tosk.TodoTask.Services;

public class TodoService([FromKeyedServices(AppMode.Live)] ITodoRepository todoRepository) : ITodoService
{
    public Task AddAsync(string task)
    {
        if (string.IsNullOrWhiteSpace(task)) return Task.CompletedTask;
        return todoRepository.AddAsync(task);
    }

    public Task<IEnumerable<Todo>> GetAllAsync() => todoRepository.GetAllAsync(
        orderings: [
            (OrderBy.Ascending, x => x.IsCompleted),
            (OrderBy.Descending, x => x.CreatedAt)
            ]);


    public Task<IEnumerable<Todo>> GetAllCompletedAsync() => todoRepository.GetAllAsync(
        filter: x => x.IsCompleted,
        orderings: [
            (OrderBy.Descending, x => x.CompletedAt),
            ]);

    public Task<IEnumerable<Todo>> GetAllImportantAsync() => todoRepository.GetAllAsync(
        filter: x => x.IsImportant,
        orderings: [
            (OrderBy.Descending, x => x.CreatedAt),
            ]);

    public Task ToggleCompletionAsync(Todo todo)
    {
        todo.ToggleCompletion();
        return todoRepository.UpdateAsync(todo);
    }

    public Task ToggleImportanceAsync(Todo todo)
    {
        todo.ToggleImportance();
        return todoRepository.UpdateAsync(todo);
    }

    public Task DeleteAsync(Todo todo)
    {
        return todoRepository.DeleteAsync(todo);
    }
}

public static class Registrar
{
    public static IServiceCollection AddTodoService(this IServiceCollection services) => services.AddSingleton<ITodoService, TodoService>();
}
