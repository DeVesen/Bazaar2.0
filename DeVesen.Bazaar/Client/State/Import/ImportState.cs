using Fluxor;

namespace DeVesen.Bazaar.Client.State.Import;

[FeatureState]
public record ImportState(IEnumerable<ImportInfo> Items)
{
    private ImportState() : this(Enumerable.Empty<ImportInfo>()) { }
}

public record ImportInfo
{
    public required Models.Article? Article { get; init; }
    public required bool Split { get; init; }
    public required bool? Validated { get; init; }
    public required bool? Imported { get; init; }
    public required int LineIndex { get; init; }
    public required string Line { get; init; }
    public required IEnumerable<string> ErrorMessages { get; init; }
}