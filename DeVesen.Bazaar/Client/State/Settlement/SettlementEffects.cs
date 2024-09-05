using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementEffects
{
    [EffectMethod]
    public async Task FetchHazardSigns(SettlementActions.FetchSettlement action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new SettlementActions.SetSettlement([]));

        //dispatcher.Dispatch(new SettlementActions.SettlementFetchFailed());

        await Task.CompletedTask;
    }
}