using Tosk.Commons.ViewModels;
using Tosk.TodoTask.Enums;
using Tosk.TodoTask.Models;
using Tosk.TodoTask.Services;

namespace Tosk.TodoTask.ViewModel;

public class TodoVM(ITodoService todoService) : BaseViewModel
{
    public string TodoTitle { get; set; } = string.Empty;
    public FilterBy FilterBy { get; set; }
    public IEnumerable<Todo> Todos { get; set; } = [];


    Task<IEnumerable<Todo>> FetchData() => FilterBy switch
    {
        FilterBy.Completed => todoService.GetAllCompletedAsync(),
        FilterBy.Important => todoService.GetAllImportantAsync(),
        _ => todoService.GetAllAsync()
    };
    public async Task LoadTodosAsync() => Todos = (await FetchData()).ToList();
    private Task<bool> ReloadTodosAsync() => LoadTodosAsync().ContinueWith(_ => true);


    public async Task AddAsync()
    {
        await todoService.AddAsync(TodoTitle);
        TodoTitle = string.Empty;
        await NotifyPropertyChangedAsync(predicate: ReloadTodosAsync);
    }

    public async Task ToggleCompletionAsync(Todo todo)
    {
        await todoService.ToggleCompletionAsync(todo);
        await NotifyPropertyChangedAsync(predicate: ReloadTodosAsync);
    }

    public async Task ToggleImportanceAsync(Todo todo)
    {
        await todoService.ToggleImportanceAsync(todo);
        await NotifyPropertyChangedAsync(predicate: ReloadTodosAsync);
    }

    public async Task DeleteAsync(Todo todo)
    {
        await todoService.DeleteAsync(todo);
        await NotifyPropertyChangedAsync(predicate: ReloadTodosAsync);
    }

    public async Task UpdateFilterAsync(FilterBy filterBy)
    {
        FilterBy = filterBy;
        await NotifyPropertyChangedAsync(predicate: ReloadTodosAsync);
    }
}

public static class Registrar
{
    public static IServiceCollection AddTodoVM(this IServiceCollection services) => services.AddSingleton<TodoVM>();
}
