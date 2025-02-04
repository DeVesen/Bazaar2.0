using Fluxor;
using SmartBazaar.Shared.Contracts;

namespace SmartBazaar.Client.States.DealerInfoState;

public class DealerInfoEffects(IDealerService dealerService, IArticleService articleService)
{
    [EffectMethod]
    public async Task ReLoadAllDealer(DealerInfoActions.ReLoadAllDealer action, IDispatcher dispatcher)
    {
        var response = await dealerService.GetAllAsync();

        if (response.IsSuccess is false)
        {
            dispatcher.Dispatch(new DealerInfoActions.ReLoadAllDealerFailed());
        }

        foreach (var element in response.Value)
        {
            dispatcher.Dispatch(new DealerInfoActions.ReLoadDealerModel(element));
        }
    }


    [EffectMethod]
    public async Task UpsertDealerById(DealerInfoActions.ReLoadDealerById action, IDispatcher dispatcher)
    {
        var dealerTask = dealerService.GetByIdAsync(action.DealerId);
        var statisticsTask = articleService.GetStatistics(action.DealerId);

        await Task.WhenAll(dealerTask, statisticsTask);

        dispatcher.Dispatch(new DealerInfoActions.UpsertDealerView(dealerTask.Result.Value, statisticsTask.Result.Value));
    }

    [EffectMethod]
    public async Task UpsertDealerModel(DealerInfoActions.ReLoadDealerModel action, IDispatcher dispatcher)
    {
        var statistics = await articleService.GetStatistics(action.DealerModel.Id);

        dispatcher.Dispatch(new DealerInfoActions.UpsertDealerView(action.DealerModel, statistics.Value));
    }


    [EffectMethod]
    public async Task UpsertDealerById(DealerInfoActions.ReLoadDealerStatistic action, IDispatcher dispatcher)
    {
        var statistics = await articleService.GetStatistics(action.DealerId);

        dispatcher.Dispatch(new DealerInfoActions.SetDealerStatistic(action.DealerId, statistics.Value));
    }
}