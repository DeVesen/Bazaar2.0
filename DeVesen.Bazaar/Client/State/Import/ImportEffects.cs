using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Import;

public class ImportEffects
{
    private readonly ArticleService _articleService;
    private readonly ImportExportService _importExportService;

    public ImportEffects(ArticleService articleService, ImportExportService importExportService)
    {
        _articleService = articleService;
        _importExportService = importExportService;
    }

    [EffectMethod]
    public Task AnalyzeDataLineAsync(ImportActions.AnalyzeDataLine action, IDispatcher dispatcher)
    {
        var lineData = action.LineData.Trim();

        if (string.IsNullOrWhiteSpace(lineData))
        {
            return Task.CompletedTask;
        }

        var isValidLine = _importExportService.TrySplitToArticle(action.VendorId, lineData, out var article);

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
        var response = await _articleService.GetByNumber(action.Info.Article!.Number);

        var success = response is { IsValid: true, Value: null };
        var errorMessage = success is false
            ? new[] { "Nummer existiert bereits!" }
            : new[] { "" };

        dispatcher.Dispatch(new ImportActions.DataLineValidated(action.Info.LineIndex, success, errorMessage));
    }

    [EffectMethod]
    public async Task ImportDataLinesAsync(ImportActions.ImportDataLines action, IDispatcher dispatcher)
    {
        foreach (var line in action.Lines)
        {
            var result = await _articleService.CreateAsync(line.Article, false);

            dispatcher.Dispatch(new ImportActions.DataLineImported(line.LineIndex, result.IsValid, result.ErrorMessages));
        }

        dispatcher.Dispatch(new ImportActions.AllIDataLinesImported());
    }
}