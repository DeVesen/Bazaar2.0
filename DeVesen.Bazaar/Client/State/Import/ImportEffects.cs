using DeVesen.Bazaar.Client.Services;
using DeVesen.Bazaar.Shared.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Import;

public class ImportEffects
{
    private readonly ArticleService _articleService;
    private readonly SystemClock _systemClock;

    public ImportEffects(ArticleService articleService, SystemClock systemClock)
    {
        _articleService = articleService;
        _systemClock = systemClock;
    }

    [EffectMethod]
    public Task AnalyzeDataLineAsync(ImportActions.AnalyzeDataLine action, IDispatcher dispatcher)
    {
        var lineData = action.LineData.Trim();

        if (string.IsNullOrWhiteSpace(lineData))
        {
            return Task.CompletedTask;
        }

        var isValidLine = TryMapToArticle(action.VendorId, lineData, out var article);

        var importInfo = new ImportInfo
        {
            Article = article,
            Split = isValidLine,
            Validated = null,
            Imported = null,
            LineIndex = action.LineIndex,
            Line = action.LineData,
            ErrorMessages = Enumerable.Empty<string>()
        };

        dispatcher.Dispatch(new ImportActions.DataLineAnalyzed(importInfo));

        return Task.CompletedTask;
    }

    [EffectMethod]
    public async Task DataLineAnalyzedAsync(ImportActions.DataLineAnalyzed action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ImportActions.DataLineValidated(action.Info.LineIndex, true, Enumerable.Empty<string>()));
    }

    [EffectMethod]
    public async Task ImportDataLineAsync(ImportActions.ImportDataLine action, IDispatcher dispatcher)
    {
        var result = await _articleService.CreateAsync(action.Article, false);

        dispatcher.Dispatch(new ImportActions.DataLineImported(action.LineIndex, result.IsValid, result.ErrorMessages));
    }

    private bool TryMapToArticle(string vendorId, string line, out Models.Article? value)
    {
        var lineParts = line.Split(new[] { ";" }, StringSplitOptions.None);

        if (lineParts.Length != 7)
        {
            value = null!;
            return false;
        }

        value = new Models.Article
        {
            VendorId = vendorId,
            Number = int.Parse(lineParts[0]),
            Title = lineParts[1],
            ArticleCategory = lineParts[2],
            Manufacturer = lineParts[3],
            Created = _systemClock.GetNow(),
            Price01 = double.Parse(lineParts[4]),
            Price02 = double.TryParse(lineParts[5], out var price02Val) ? price02Val : null,
            Description = lineParts[6]
        };

        return true;
    }
}