using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementFacade(IDispatcher dispatcher)
{
    public void Fetch(string vendorId) => dispatcher.Dispatch(new SettlementActions.FetchSettlement(vendorId));
}