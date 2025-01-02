﻿using Tosk.TodoTask.Models;
using Tosk.Enums;

namespace Tosk.TodoTask.Repositories
{
    namespace InMemory
    {
        using Tosk.Commons.Repositories.Sources.InMemory;
        using Tosk.TodoTask.Seeders;

        public class TodoRepository : BaseRepository<Todo, Guid>, ITodoRepository
        {
            public TodoRepository() : base(TodoSeeder.GenerateTodos()) { }
        }

        public static class Registrar
        {
            public static IServiceCollection AddInMemoryTodoRepository(this IServiceCollection services) => services.AddKeyedSingleton<ITodoRepository, TodoRepository>(nameof(AppMode.Demo));
        }
    }

    namespace SQLite
    {
        using Microsoft.Extensions.DependencyInjection;
        using Tosk.Commons.Repositories.Sources.Sqlite;
        using Tosk.Commons.SQLite;

        public class TodoRepository(BaseDbContext dbContext) : BaseRepository<Todo, Guid>(dbContext), ITodoRepository;

        public static class Registrar
        {
            public static IServiceCollection AddSqliteTodoRepository(this IServiceCollection services) => services.AddKeyedSingleton<ITodoRepository, TodoRepository>(nameof(AppMode.Live));
        }
    }  
}