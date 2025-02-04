using Fluxor;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.DealerInfoState;

public static class DealerInfoReducer
{
    [ReducerMethod(typeof(DealerInfoActions.ReLoadAllDealer))]
    public static DealerInfoState FetchAll(DealerInfoState state)
        => state with { Elements = [], IsLoading = true };

    [ReducerMethod(typeof(DealerInfoActions.ReLoadAllDealerFailed))]
    public static DealerInfoState FetchAllFailed(DealerInfoState state)
        => state with { IsLoading = false };


    [ReducerMethod]
    public static DealerInfoState SetDealerView(DealerInfoState state, DealerInfoActions.UpsertDealerView action)
    {
        var elements = state.Elements.ToList();

        var elementIndex = elements.FindIndex(0, p => p.Dealer.Id == action.DealerModel.Id);

        if (elementIndex < 0)
        {
            elements.Add(new DealerInfoAggregate
            {
                Dealer = action.DealerModel,
                Statistic = action.StatisticModel
            });
        }
        else
        {
            elements[elementIndex].Dealer = action.DealerModel;
            elements[elementIndex].Statistic = action.StatisticModel;
        }

        return state with { Elements = elements, IsLoading = false};
    }


    [ReducerMethod]
    public static DealerInfoState SetDealerStatistic(DealerInfoState state, DealerInfoActions.SetDealerStatistic action)
    {
        var elements = state.Elements.ToList();

        var elementIndex = elements.FindIndex(0, p => p.Dealer.Id == action.DealerId);

        if (elementIndex >= 0)
        {
            elements[elementIndex].Statistic = action.StatisticModel;
        }

        return state with { Elements = elements, IsLoading = false};
    }
    

    [ReducerMethod]
    public static DealerInfoState RemoveDealer(DealerInfoState state, DealerInfoActions.RemoveDealer action)
    {
        var elements = state.Elements.ToList();

        elements.RemoveAll(p => p.Dealer.Id == action.Id);

        return state with { Elements = elements };
    }
}