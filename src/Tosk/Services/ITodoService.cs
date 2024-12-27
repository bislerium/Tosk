using Tosk.Models;

namespace Tosk.Services;

public interface ITodoService
{
    //Create
    void Add(string task);

    //Read
    IEnumerable<Todo> GetAll();
    IEnumerable<Todo> GetCompletedTasks();
    IEnumerable<Todo> GetImportantTasks();
}
