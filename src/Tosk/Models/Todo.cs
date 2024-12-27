﻿namespace Tosk.Models;

public class Todo
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
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