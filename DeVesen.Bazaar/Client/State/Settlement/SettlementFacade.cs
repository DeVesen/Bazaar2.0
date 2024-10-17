using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementFacade(IDispatcher dispatcher)
{
    public void Fetch(string vendorId) => dispatcher.Dispatch(new SettlementActions.FetchSettlement(vendorId));

    public void PayOut(string vendorId) => dispatcher.Dispatch(new SettlementActions.PayOut(vendorId));

    public void ResetSelection() => dispatcher.Dispatch(new SettlementActions.ResetSelection());
}