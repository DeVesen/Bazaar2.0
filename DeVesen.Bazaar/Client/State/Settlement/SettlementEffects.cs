using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementEffects(VendorService vendorService, ArticleService articleService)
{
    [EffectMethod]
    public async Task FetchHazardSigns(SettlementActions.FetchSettlement action, IDispatcher dispatcher)
    {
        var vendorViewResult = await vendorService.GetByIdAsync(action.VendorId);
        if (vendorViewResult.IsValid is false)
        {
            dispatcher.Dispatch(new SettlementActions.SettlementFetchFailed());
            return;
        }

        var articlesResult = await articleService.GetAllAsync(vendorId: action.VendorId);
        if (articlesResult.IsValid is false)
        {
            dispatcher.Dispatch(new SettlementActions.SettlementFetchFailed());
            return;
        }

        dispatcher.Dispatch(new SettlementActions.SetSettlement(vendorViewResult.Value, articlesResult.Value));
    }
}