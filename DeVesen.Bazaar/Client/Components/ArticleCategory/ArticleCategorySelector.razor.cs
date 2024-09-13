using MudBlazor;

namespace DeVesen.Bazaar.Client.Components.ArticleCategory
{
    public partial class ArticleCategorySelector : MudFormComponent<string, string>
    {
        public ArticleCategorySelector() : base(new DefaultConverter<string>())
        {

        }
    }
}
