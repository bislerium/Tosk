﻿using TodoModel = Tosk.Models.Todo;

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
        _tasks.Add(task);
    }

    public IEnumerable<TodoModel> GetAll() => _tasks
        .OrderBy(x => x.IsCompleted)
        .ThenByDescending(x => x.IsCompleted ? x.CompletedAt : x.CreatedAt)
        .ToArray();

    public IEnumerable<TodoModel> GetCompletedTasks() => _tasks
        .Where(x => x.IsCompleted)
        .ToArray();

    public IEnumerable<TodoModel> GetImportantTasks() => _tasks
        .Where(x => x.IsImportant)
        .ToArray();
}