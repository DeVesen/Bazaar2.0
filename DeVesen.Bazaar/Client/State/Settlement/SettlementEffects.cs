using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementEffects(VendorService vendorService, ArticleService articleService)
{
    [EffectMethod]
    public async Task FetchSettlement(SettlementActions.FetchSettlement action, IDispatcher dispatcher)
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

        var articles = articlesResult.Value.Where(p => p.IsApprovedForSale())
                                           .ToArray();

        dispatcher.Dispatch(new SettlementActions.SetSettlement(vendorViewResult.Value, articles));
    }

    [EffectMethod]
    public async Task PayOut(SettlementActions.PayOut action, IDispatcher dispatcher)
    {
        await vendorService.SettleAsync(action.VendorId, action.ArticleIds);

        dispatcher.Dispatch(new SettlementActions.FetchSettlement(action.VendorId));
    }
}