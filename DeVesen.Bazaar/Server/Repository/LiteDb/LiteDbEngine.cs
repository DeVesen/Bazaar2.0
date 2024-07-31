using LiteDB;

namespace DeVesen.Bazaar.Server.Repository.LiteDb;

public class LiteDbEngine : ILiteDbEngine, IDisposable
{
    public readonly LiteDatabase Engine;

    private bool _isDisposed;

    public LiteDbEngine(string dbFile)
    {
        Engine = new LiteDatabase(dbFile);
    }

    public ILiteCollection<T> GetCollection<T>(string name, BsonAutoId autoId = BsonAutoId.ObjectId)
        => Engine.GetCollection<T>(name, autoId);

    public ILiteCollection<T> GetCollection<T>()
        => Engine.GetCollection<T>();

    public ILiteCollection<T> GetCollection<T>(BsonAutoId autoId)
        => Engine.GetCollection<T>(autoId);

    public ILiteCollection<BsonDocument> GetCollection(string name, BsonAutoId autoId
        = BsonAutoId.ObjectId) => Engine.GetCollection(name, autoId);

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;

        Engine.Dispose();
    }
}

public interface ILiteDbEngine
{
    public ILiteCollection<T> GetCollection<T>(string name, BsonAutoId autoId = BsonAutoId.ObjectId);

    public ILiteCollection<T> GetCollection<T>();

    public ILiteCollection<T> GetCollection<T>(BsonAutoId autoId);

    public ILiteCollection<BsonDocument> GetCollection(string name, BsonAutoId autoId = BsonAutoId.ObjectId);
}