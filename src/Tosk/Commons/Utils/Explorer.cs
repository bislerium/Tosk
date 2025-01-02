namespace Tosk.Commons.Utils;

public static class Explorer
{
    private static readonly string _cacheDirectoryPath = FileSystem.Current.CacheDirectory;
    public static string GetFilePath(string fileName, string extension) => Path.Combine(_cacheDirectoryPath, Path.ChangeExtension(fileName, extension));
}
