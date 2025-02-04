using Fluxor;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.ArticleState;

public static class ArticleSelector
{
    public static bool IsLoading(this IState<ArticleState> state)
        => state.Value.IsLoading;


    public static void WaitLoadingIsDone(this IState<ArticleState> state)
        => Supporter.WaitUntil(() => state.IsLoading() is false);


    public static IEnumerable<ArticleModel> GetAll(this IState<ArticleState> state)
    {
        return state.Value.Elements;
    }
}