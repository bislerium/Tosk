using Tosk.Commons.SQLite;
using Tosk.TodoTask.Models;

namespace Tosk.SQLite;

public class DbContext : BaseDbContext
{
    private const string DatabaseName = nameof(Todo) + "_db";

    public DbContext() : base(DatabaseName)
    {
    }

    public override Task Initialize() => Database.CreateTablesAsync(types: typeof(Todo));
}

public static class Registrar
{
    public static void AddSQLiteDbContext(this IServiceCollection services) => services.AddSingleton<BaseDbContext, DbContext>();
}
