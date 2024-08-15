using DeVesen.Bazaar.Client.Services;
using DeVesen.Bazaar.Shared;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.SalesCart;

public class SalesCartEffects
{
    private readonly IState<SalesCartState> _state;
    private readonly ArticleService _articleService;

    public SalesCartEffects(IState<SalesCartState> state, ArticleService articleService)
    {
        _state = state;
        _articleService = articleService;
    }

    [EffectMethod]
    public Task AnalyzeDataLineAsync(SalesCartActions.RequestItemToCart action, IDispatcher dispatcher)
    {
        if (_state.Value.PurchaseItems.Any(p => p.ArticleNumber == action.ArticleNumber))
        {
            dispatcher.Dispatch(new SalesCartActions.AddItemToCartFailed(action.ArticleNumber, action.SalesAmount, $"Artikel '{action.ArticleNumber}' bereits im Warenkorb!"));

            return Task.CompletedTask;
        }

        dispatcher.Dispatch(new SalesCartActions.AddItemToCart(action.ArticleNumber, action.SalesAmount, action.Article));

        return Task.CompletedTask;
    }

    [EffectMethod(typeof(SalesCartActions.CompleteSale))]
    public async Task AnalyzeDataLineAsync(IDispatcher dispatcher)
    {
        var soldItems =
            _state.Value.PurchaseItems
                .Select(p => new SalesOrderDto.Position(p.ArticleNumber, p.SalesAmount))
                .ToArray();

        await _articleService.BookOrderAsync(soldItems);

        dispatcher.Dispatch(new SalesCartActions.SaleCompleted());
    }
}