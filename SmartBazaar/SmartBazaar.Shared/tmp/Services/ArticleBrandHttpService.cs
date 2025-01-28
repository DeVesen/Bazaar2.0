using SmartBazaar.Shared.Dto;

namespace SmartBazaar.Client.Services;

public class ArticleBrandHttpService
{
    public async Task<(bool IsSuccess, ArticleBrandDto[] Elements)> GetAllAsync()
    {
        var result = new[]
        {
            CreateElement("Head", string.Empty),
            CreateElement("Atomic", string.Empty),
            CreateElement("K2", string.Empty)
        };

        return await Task.FromResult((true, result));
    }


    private static ArticleBrandDto CreateElement(string title, string url)
        => new()
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            Url = url
        };
}