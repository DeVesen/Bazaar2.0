namespace DeVesen.Bazaar.Client.State.Import;

public class ImportActions
{
    public record ClearBuffer;

    public record AnalyzeDataLine(string VendorId, int LineIndex, string LineData);

    public record DataLineAnalyzed(ImportInfo Info);

    public record DataLineValidated(int LineIndex, bool Success, IEnumerable<string> ErrorMessages);

    public record ImportDataLine(int LineIndex, Models.Article Article);

    public record DataLineImported(int LineIndex, bool Success, IEnumerable<string> ErrorMessages);
}