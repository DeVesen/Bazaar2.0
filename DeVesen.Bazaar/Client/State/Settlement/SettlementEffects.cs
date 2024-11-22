using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementEffects(SettlementService settlementService)
{
    [EffectMethod]
    public async Task FetchSettlement(SettlementActions.FetchSettlement action, IDispatcher dispatcher)
    {
        var vendorViewResult = await settlementService.GetAsync(action.VendorId);

        if (vendorViewResult.IsSuccess is false)
        {
            dispatcher.Dispatch(new SettlementActions.SettlementFetchFailed());
            return;
        }

        dispatcher.Dispatch(new SettlementActions.SetSettlement
        {
            Vendor = vendorViewResult.Data.Vendor,
            ArticleStock = vendorViewResult.Data.ArticleStock,
            ArticleValue = vendorViewResult.Data.ArticleValue,
            Articles = vendorViewResult.Data.Articles.ToArray()
        });
    }

    [EffectMethod]
    public async Task PayOut(SettlementActions.PayOut action, IDispatcher dispatcher)
    {
        await settlementService.SetVendorAsSettledAsync(action.VendorId);

        dispatcher.Dispatch(new SettlementActions.FetchSettlement(action.VendorId));
    }
}