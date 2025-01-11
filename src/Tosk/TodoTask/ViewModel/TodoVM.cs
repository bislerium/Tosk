using Tosk.Commons.Enums;
using Tosk.Commons.ViewModels;
using Tosk.TodoTask.Enums;
using Tosk.TodoTask.Models;
using Tosk.TodoTask.Services;
using Tosk.TodoTask.Services.Resolver;

namespace Tosk.TodoTask.ViewModel;

public class TodoVM(ITodoServiceResolver todoServiceResolver) : BaseViewModel
{
    private const AppMode DefaultAppMode = AppMode.Live;
    private ITodoService _todoService = todoServiceResolver.Resolve(DefaultAppMode);

    public string TodoTitle { get; set; } = string.Empty;
    public FilterBy FilterBy { get; set; }
    public AppMode AppMode { get; set; } = DefaultAppMode;
    public IEnumerable<Todo> Todos { get; set; } = [];

    Task<IEnumerable<Todo>> FetchData() => FilterBy switch
    {
        FilterBy.Completed => _todoService.GetAllCompletedAsync(),
        FilterBy.Important => _todoService.GetAllImportantAsync(),
        _ => _todoService.GetAllAsync()
    };
    public async Task LoadTodosAsync() => Todos = (await FetchData()).ToList();
    private Task<bool> ReloadTodosAsync() => LoadTodosAsync().ContinueWith(_ => true);

    public async Task ToggleAppMode()
    {
        AppMode = AppMode == AppMode.Live ? AppMode.Demo : AppMode.Live;
        _todoService = todoServiceResolver.Resolve(AppMode);
        await NotifyPropertyChangedAsync(predicate: ReloadTodosAsync);
    }

    public async Task AddAsync()
    {
        await _todoService.AddAsync(TodoTitle);
        TodoTitle = string.Empty;
        await NotifyPropertyChangedAsync(predicate: ReloadTodosAsync);
    }

    public async Task ToggleCompletionAsync(Todo todo)
    {
        await _todoService.ToggleCompletionAsync(todo);
        await NotifyPropertyChangedAsync(predicate: ReloadTodosAsync);
    }

    public async Task ToggleImportanceAsync(Todo todo)
    {
        await _todoService.ToggleImportanceAsync(todo);
        await NotifyPropertyChangedAsync(predicate: ReloadTodosAsync);
    }

    public async Task DeleteAsync(Todo todo)
    {
        await _todoService.DeleteAsync(todo);
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
