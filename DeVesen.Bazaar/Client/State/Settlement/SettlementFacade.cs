using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementFacade(IDispatcher dispatcher)
{
    public void Fetch(string vendorId) => dispatcher.Dispatch(new SettlementActions.FetchSettlement(vendorId));

    public void PayOut(string vendorId, IEnumerable<string> articleIds) => dispatcher.Dispatch(new SettlementActions.PayOut(vendorId, articleIds));

    public void ResetSelection() => dispatcher.Dispatch(new SettlementActions.ResetSelection());
}