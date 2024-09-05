using DeVesen.Bazaar.Client.State.Article;
using DeVesen.Bazaar.Client.State.ArticleCategory;
using DeVesen.Bazaar.Client.State.Manufacturer;
using DeVesen.Bazaar.Client.State.VendorView;

namespace DeVesen.Bazaar.Client.State
{
    public class StateFacade
    {
        private readonly VendorViewFacade _vendorFacade;
        private readonly ArticleFacade _articleFacade;
        private readonly ManufacturerFacade _manufacturerFacade;
        private readonly ArticleCategoryFacade _articleCategoryFacade;

        public VendorViewFacade Vendor => _vendorFacade;
        public ArticleFacade Article => _articleFacade;
        public ManufacturerFacade Manufacturer => _manufacturerFacade;
        public ArticleCategoryFacade ArticleCategory => _articleCategoryFacade;

        public StateFacade(VendorViewFacade vendorFacade, ArticleFacade articleFacade, ManufacturerFacade manufacturerFacade, ArticleCategoryFacade articleCategoryFacade)
        {
            _vendorFacade = vendorFacade;
            _articleFacade = articleFacade;
            _manufacturerFacade = manufacturerFacade;
            _articleCategoryFacade = articleCategoryFacade;
        }

        public void FetchAll()
        {
            _vendorFacade.Fetch();
            _articleFacade.Fetch();
            _manufacturerFacade.Fetch();
            _articleCategoryFacade.Fetch();
        }
    }
}
