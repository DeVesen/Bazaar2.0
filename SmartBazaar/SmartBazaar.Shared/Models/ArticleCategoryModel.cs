namespace SmartBazaar.Shared.Models;

public class ArticleCategoryModel : ArticleCategoryDataModel
{
    public required string Id { get; init; }
}

public class ArticleCategoryCreateModel : ArticleCategoryDataModel
{
}

public class ArticleCategoryDataModel
{
    public string Title { get; set; } = string.Empty;

    public string? ImgSrc { get; set; }
}