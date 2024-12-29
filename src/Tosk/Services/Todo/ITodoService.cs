using TodoModel =  Tosk.Models.Todo;

namespace Tosk.Services.Todo;

public interface ITodoService
{
    //Create
    void Add(string task);

    //Read
    IEnumerable<TodoModel> GetAll();
    IEnumerable<TodoModel> GetAllCompleted();
    IEnumerable<TodoModel> GetAllImportant();
}
