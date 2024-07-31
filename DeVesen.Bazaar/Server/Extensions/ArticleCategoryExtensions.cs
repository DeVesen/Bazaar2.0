using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared;

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

    public static ArticleCategory ToDomain(this (Guid id, ArticleCategoryCreateDto dto) data)
        => new()
        {
            Id = data.id,
            Name = data.dto.Name
        };

    public static ArticleCategory ToDomain(this (Guid id, ArticleCategoryUpdateDto dto) data)
        => new()
        {
            Id = data.id,
            Name = data.dto.Name
        };

    public static ArticleCategoryDto ToDto(this ArticleCategory data)
        => new(data.Id, data.Name);
}