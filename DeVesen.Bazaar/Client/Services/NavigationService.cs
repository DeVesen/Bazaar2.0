using Microsoft.AspNetCore.Components;

namespace DeVesen.Bazaar.Client.Services;

public class NavigationService
{
    private readonly NavigationManager _navigationManager;

    public NavigationService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public VendorNavigationService Vendor => new(_navigationManager);
    public ArticleNavigationService Article => new(_navigationManager);
    public ArticleCategoryNavigationService ArticleCategory => new(_navigationManager);


    public class ArticleCategoryNavigationService : ChildNavigationService
    {
        public ArticleCategoryNavigationService(NavigationManager navigationManager) : base(navigationManager)
        {
        }

        public void ToOverview()
        {
            NavigationManager.NavigateTo("article-category");
        }
    }

    public class VendorNavigationService : ChildNavigationService
    {
        public VendorNavigationService(NavigationManager navigationManager) : base(navigationManager)
        {
        }

        public void ToOverview()
        {
            NavigationManager.NavigateTo("vendors");
        }

        public void ToArticle(string vendorId)
        {
            NavigationManager.NavigateTo($"vendors/{vendorId}/articles");
        }
    }

    public class ArticleNavigationService : ChildNavigationService
    {
        public ArticleNavigationService(NavigationManager navigationManager) : base(navigationManager)
        {
        }

        public void ToOverview()
        {
            NavigationManager.NavigateTo("articles");
        }
    }

    public class ChildNavigationService
    {
        protected readonly NavigationManager NavigationManager;

        public ChildNavigationService(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }
    }
}