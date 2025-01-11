using Tosk.Commons.Enums;
using Tosk.TodoTask.Repositories;

namespace Tosk.TodoTask.Services.Resolver;

public class TodoServiceResolver(IServiceProvider serviceProvider) : ITodoServiceResolver
{
    public ITodoService Resolve(AppMode appMode)
    {
        var repository = serviceProvider.GetRequiredKeyedService<ITodoRepository>(appMode);
        return new TodoService(repository);
    }
}

public static class Registrar
{        
    public static IServiceCollection AddTodoServiceResolver(this IServiceCollection services) => services.AddSingleton<ITodoServiceResolver, TodoServiceResolver>();
}
