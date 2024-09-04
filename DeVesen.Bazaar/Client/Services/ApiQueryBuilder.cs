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

public class UriBuilder
{
    private readonly List<string> _uriParts = [];
    private readonly Dictionary<string, string?> _queryItems = new();

    public UriBuilder()
    {
        
    }

    public UriBuilder(string baseUri)
    {
        AddUriPart(baseUri);
    }

    public UriBuilder(Uri? baseUri)
    {
        if (baseUri != null)
        {
            AddUriPart(baseUri.ToString());
        }
    }

    public UriBuilder SetQueryItem(string key, string? value)
    {
        _queryItems[key] = value;

        return this;
    }

    public UriBuilder AddUriPart(string part)
    {
        if (string.IsNullOrWhiteSpace(part))
        {
            part = string.Empty;
        }

        _uriParts.Add(part.Trim('/'));

        return this;
    }

    public string Build()
        => BuildUri() + BuildQuery();

    private IEnumerable<string> GetQueryParts()
        => _queryItems.Where(p => p.Value != null).Select(item => $"{item.Key}={item.Value}");

    private string BuildUri()
    {
        var parts = _uriParts.Where(p => string.IsNullOrWhiteSpace(p) is false)
                             .Select(p => p.Trim().Trim('/'));
        return string.Join("/", parts);
    }

    private string BuildQuery()
    {
        var queryParts = GetQueryParts().ToArray();

        return queryParts.Length != 0
            ? "?" + string.Join("&", queryParts)
            : string.Empty;
    }
}