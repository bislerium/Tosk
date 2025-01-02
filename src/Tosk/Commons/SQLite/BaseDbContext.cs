using SQLite;
using Tosk.Commons.Utils;

namespace Tosk.Commons.SQLite;

public abstract class BaseDbContext(string DatabaseName)
{
    private readonly string _databasePath = Explorer.GetFilePath(DatabaseName, ".sqlite");

    private const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite
        | SQLiteOpenFlags.Create
        | SQLiteOpenFlags.SharedCache;

    public SQLiteAsyncConnection Database => new(_databasePath, Flags);

    public abstract Task Initialize();
}
