using Microsoft.AspNetCore.Components;

namespace DeVesen.Bazaar.Client.Services;

public class NavigationService
{
    private readonly NavigationManager _navigationManager;

    public NavigationService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public SalesNavigationService Sales => new(_navigationManager);
    public VendorNavigationService Vendor => new(_navigationManager);


    public class SalesNavigationService : ChildNavigationService
    {
        public SalesNavigationService(NavigationManager navigationManager) : base(navigationManager) { }

        public void ToTheSale()
        {
            NavigationManager.NavigateTo("Sales");
        }
    }

    public class VendorNavigationService : ChildNavigationService
    {
        public VendorNavigationService(NavigationManager navigationManager) : base(navigationManager) { }

        public void ToArticle(string vendorId)
        {
            NavigationManager.NavigateTo($"vendors/{vendorId}/articles");
        }

        public void ToSettlement(string vendorId)
        {
            NavigationManager.NavigateTo($"vendors/{vendorId}/settlement");
        }
    }

    public abstract class ChildNavigationService
    {
        protected readonly NavigationManager NavigationManager;

        protected ChildNavigationService(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }
    }
}