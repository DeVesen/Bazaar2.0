using Fluxor;
using SmartBazaar.Client.States.ArticleState;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.DealerInfoState;

public class DealerInfoFacade(IDispatcher dispatcher)
{
    public void ReLoadAllDealer()
        => dispatcher.Dispatch(new DealerInfoActions.ReLoadAllDealer());


    public void ReLoadDealer(string dealerId)
        => dispatcher.Dispatch(new DealerInfoActions.ReLoadDealerById(dealerId));

    public void ReLoadDealer(DealerModel element)
        => dispatcher.Dispatch(new DealerInfoActions.ReLoadDealerModel(element));


    public void ReLoadDealerStatistic(string dealerId)
        => dispatcher.Dispatch(new DealerInfoActions.ReLoadDealerStatistic(dealerId));


    public void RemoveDealer(string dealerId)
    {
        dispatcher.Dispatch(new DealerInfoActions.RemoveDealer(dealerId));
        dispatcher.Dispatch(new ArticleActions.RemoveDealer(dealerId));
    }
}