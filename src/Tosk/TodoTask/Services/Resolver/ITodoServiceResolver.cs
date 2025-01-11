using Tosk.Commons.Enums;

namespace Tosk.TodoTask.Services.Resolver
{
    public interface ITodoServiceResolver
    {
        ITodoService Resolve(AppMode appMode);
    }
}
