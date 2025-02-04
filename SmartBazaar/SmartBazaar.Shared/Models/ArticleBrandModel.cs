namespace SmartBazaar.Shared.Models;

public class ArticleBrandModel : ArticleBrandDataModel
{
    public required string Id { get; init; }
}

public class ArticleBrandCreateModel : ArticleBrandDataModel
{
}

public class ArticleBrandDataModel
{
    public string Title { get; set; } = string.Empty;

    public string? ImgSrc { get; set; }
}