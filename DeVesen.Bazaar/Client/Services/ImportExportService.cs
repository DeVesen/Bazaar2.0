using System.Text;
using DeVesen.Bazaar.Shared.Services;

namespace DeVesen.Bazaar.Client.Services;

public class ImportExportService(SystemClock systemClock)
{
    public bool TrySplitToArticle(string vendorId, string line, out Models.Article value)
    {
        var lineParts = line.Split([";"], StringSplitOptions.None);

        if (lineParts.Length < 5)
        {
            value = null!;
            return false;
        }

        value = new Models.Article
        {
            VendorId = vendorId,
            Number = int.Parse(lineParts[0]),
            Description = lineParts[1],
            ArticleCategory = lineParts[2],
            Manufacturer = lineParts[3],

            Price01 = double.Parse(lineParts[4]),
            Price02 = lineParts.Length == 6 && double.TryParse(lineParts[5], out var price02Val) ? price02Val : null,
            Created = systemClock.GetNow()
        };

        return true;
    }

    public string ArticleToDataLineTitle()
    {
        var lineBuilder = new StringBuilder();

        lineBuilder.Append("Nummer").Append(";");
        lineBuilder.Append("Bezeichnung").Append(";");
        lineBuilder.Append("Kategorie").Append(";");
        lineBuilder.Append("Hersteller").Append(";");
        lineBuilder.Append("Preis in €").Append(";");
        lineBuilder.Append("min. Preis in €");

        return lineBuilder.ToString();
    }

    public string ArticleToDataLine(Models.Article article)
    {
        var lineBuilder = new StringBuilder();

        lineBuilder.Append(article.Number).Append(";");
        lineBuilder.Append(article.Description).Append(";");
        lineBuilder.Append(article.ArticleCategory).Append(";");
        lineBuilder.Append(article.Manufacturer).Append(";");
        lineBuilder.Append(article.Price01).Append(";");
        lineBuilder.Append(article.Price02);

        return lineBuilder.ToString();
    }
}