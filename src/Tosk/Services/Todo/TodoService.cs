using TodoModel = Tosk.Models.Todo;

namespace Tosk.Services.Todo;

public class TodoService : ITodoService
{
    //  In-memory data source
    private readonly List<TodoModel> _tasks =
            [
            "Complete project documentation",
            "Review pull requests",
            "Prepare for the team meeting",
            "Test new application features",
            "Update project backlog",
            "Refactor old codebase",
            "Write unit tests for new modules",
            "Research new technology trends",
            "Schedule a one-on-one meeting with the manager",
            "Organize the code repository"
            ];

    public void Add(string task)
    {
        if (string.IsNullOrWhiteSpace(task)) return;
        _tasks.Add(task);
    }

    public IEnumerable<TodoModel> GetAll() => _tasks
        .OrderBy(x => x.IsCompleted)
        .ThenByDescending(x => x.IsCompleted ? x.CompletedAt : x.CreatedAt);

    public IEnumerable<TodoModel> GetAllCompleted() => _tasks
        .Where(x => x.IsCompleted)
        .OrderBy(x => x.CompletedAt);

    public IEnumerable<TodoModel> GetAllImportant() => _tasks
        .Where(x => x.IsImportant)
        .OrderBy(x => x.CreatedAt);
}
