using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;

namespace DeVesen.Bazaar.Client.Extensions;

public static class ArticleCategoryCategoryCategoryExtensions
{
    public static ArticleCategoryCreateDto ToCreateDto(this ArticleCategory data)
    {
        return new ArticleCategoryCreateDto
        {
            Name = data.Name
        };
    }

    public static ArticleCategoryUpdateDto ToUpdateDto(this ArticleCategory data)
    {
        return new ArticleCategoryUpdateDto
        {
            Name = data.Name
        };
    }

    public static ArticleCategory ToDomain(this ArticleCategoryDto dto)
    {
        return new ArticleCategory
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }
}