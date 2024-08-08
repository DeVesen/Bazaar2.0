namespace DeVesen.Bazaar.Client.Services;

public class ApiQueryBuilder
{
    private readonly Dictionary<string, string?> _innerDict = new();

    public string? this[string key]
    {
        get => _innerDict.TryGetValue(key, out var value) ? value : null;
        set => _innerDict[key] = value;
    }

    public void Set(string key, string value)
    {
        _innerDict[key] = value;
    }

    public string Build()
    {
        var queryParts = GetQueryParts();
        var result = queryParts.Aggregate("", (current, next) => current + "&" + next);
        return result.TrimStart('&').Trim();
    }

    public string BuildFinal()
        => "?" + Build();

    private IEnumerable<string> GetQueryParts()
        => _innerDict.Where(p => p.Value != null).Select(item => $"{item.Key}={item.Value}");
}