using DeVesen.Bazaar.Client.Models;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public class ArticleCategoryActions
{
    public record FetchArticleCategories(ArticleCategoryFilter Filter);

    public record ArticleCategoriesFetched(IEnumerable<Models.ArticleCategory> Items);
}