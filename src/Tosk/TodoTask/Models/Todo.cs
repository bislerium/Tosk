using SQLite;
using Tosk.Commons.Entities;

namespace Tosk.TodoTask.Models;

public class Todo : IEntity<Guid>
{
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public bool IsImportant { get; set; }

    public void ToggleCompletion()
    {
        IsCompleted = !IsCompleted;
        CompletedAt = IsCompleted ? DateTime.Now : null;
    }

    public void ToggleImportance() => IsImportant = !IsImportant;

    public static implicit operator Todo(string title) => new() { Title = title };
}
