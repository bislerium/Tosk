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

    protected override Task RefreshAsync() => RefreshDataAsync();

    Task<IEnumerable<Todo>> FetchData() => FilterBy switch
    {
        FilterBy.Completed => todoService.GetAllCompletedAsync(),
        FilterBy.Important => todoService.GetAllImportantAsync(),
        _ => todoService.GetAllAsync()
    };

    async Task RefreshDataAsync() => Todos = (await FetchData()).ToList();
    public Task LoadDataAsync() => RefreshDataAsync();

    public async Task AddAsync()
    {
        await todoService.AddAsync(TodoTitle);
        TodoTitle = string.Empty;
        await OnPropertyChanged();
    }

    public async Task ToggleCompletionAsync(Todo todo)
    {
        await todoService.ToggleCompletionAsync(todo);
        await OnPropertyChanged();
    }

    public async Task ToggleImportanceAsync(Todo todo)
    {
        await todoService.ToggleImportanceAsync(todo);
        await OnPropertyChanged();
    }

    public Task UpdateFilterAsync(FilterBy filterBy)
    {
        FilterBy = filterBy;
        return OnPropertyChanged();
    }
}

public static class Registrar
{
    public static IServiceCollection AddTodoVM(this IServiceCollection services) => services.AddSingleton<TodoVM>();
}
