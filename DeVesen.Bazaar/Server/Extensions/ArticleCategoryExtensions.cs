using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared;
using System.Text;
using DeVesen.Bazaar.Shared.Basics;

namespace DeVesen.Bazaar.Server.Extensions;

public static class ArticleCategoryExtensions
{
    public static ArticleCategoryEntity ToEntity(this ArticleCategory entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name
        };

    public static ArticleCategory ToDomain(this ArticleCategoryEntity entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name
        };

    public static ArticleCategory ToDomain(this ArticleCategoryCreateDto dto)
        => new()
        {
            Id = dto.ToShortHash(),
            Name = dto.Name
        };

    public static ArticleCategory ToDomain(this (string id, ArticleCategoryUpdateDto dto) data)
        => new()
        {
            Id = data.id,
            Name = data.dto.Name
        };

    public static ArticleCategoryDto ToDto(this ArticleCategory data)
        => new()
        {
            Id = data.Id,
            Name = data.Name
        };

    private static string ToShortHash(this ArticleCategoryCreateDto dto)
    {
        var builder = new StringBuilder();

        builder.Append(dto.Name);
        builder.Append(Guid.NewGuid().ToString());
        builder.Append(DateTime.UtcNow.Ticks);

        return builder.ToString().ToShortHash();
    }
}