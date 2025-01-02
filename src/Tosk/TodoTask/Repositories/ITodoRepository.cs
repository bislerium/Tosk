using Tosk.Commons.Repositories;
using Tosk.TodoTask.Models;

namespace Tosk.TodoTask.Repositories
{
    public interface ITodoRepository: IRepository<Todo, Guid>;
}
