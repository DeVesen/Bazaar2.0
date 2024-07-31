namespace DeVesen.Bazaar.Server;

public static class AppEnvironment
{
    public static string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;

    public static string DataDirectory => Path.Combine(BaseDirectory, "data");

    public static string GetDataFilePath(string dbFile) => Path.Combine(DataDirectory, dbFile);
}