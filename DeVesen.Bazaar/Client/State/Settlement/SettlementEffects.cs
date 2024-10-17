using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementEffects(SettlementService settlementService)
{
    [EffectMethod]
    public async Task FetchSettlement(SettlementActions.FetchSettlement action, IDispatcher dispatcher)
    {
        var vendorViewResult = await settlementService.GetAsync(action.VendorId);

        if (vendorViewResult.IsValid is false)
        {
            dispatcher.Dispatch(new SettlementActions.SettlementFetchFailed());
            return;
        }

        dispatcher.Dispatch(new SettlementActions.SetSettlement
        {
            Vendor = vendorViewResult.Value.Vendor,
            ArticleStock = vendorViewResult.Value.ArticleStock,
            ArticleValue = vendorViewResult.Value.ArticleValue,
            Articles = vendorViewResult.Value.Articles.ToArray()
        });
    }

    [EffectMethod]
    public async Task PayOut(SettlementActions.PayOut action, IDispatcher dispatcher)
    {
        await settlementService.SetAsSettledAsync(action.VendorId);

        dispatcher.Dispatch(new SettlementActions.FetchSettlement(action.VendorId));
    }
}