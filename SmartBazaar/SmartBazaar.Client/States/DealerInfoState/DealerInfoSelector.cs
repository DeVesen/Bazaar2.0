using Fluxor;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.DealerInfoState;

public static class DealerInfoSelector
{
    public static bool IsLoading(this IState<DealerInfoState> state)
        => state.Value.IsLoading;


    public static void WaitLoadingIsDone(this IState<DealerInfoState> state)
        => Supporter.WaitUntil(() => state.IsLoading() is false);


    public static string GetDealerNameById(this IState<DealerInfoState> state, string dealerId)
    {
        var dealerElement = state.Value.Elements.First(p => p.Dealer.Id.Equals(dealerId, StringComparison.OrdinalIgnoreCase));
        return $"{dealerElement.Dealer.Name.LastName}, {dealerElement.Dealer.Name.FirstName}";
    }


    public static IEnumerable<DealerInfoAggregate> GetAll(this IState<DealerInfoState> state)
    {
        return state.Value.Elements;
    }
}