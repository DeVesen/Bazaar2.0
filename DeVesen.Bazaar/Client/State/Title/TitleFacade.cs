using Fluxor;
using MudBlazor;

namespace DeVesen.Bazaar.Client.State.Title;

public class TitleFacade(IDispatcher dispatcher)
{
    public IDispatcher Dispatcher { get; } = dispatcher;

    public void SetCaption(string value)
        => Dispatcher.Dispatch(new TitleActions.SetCaption(value));
}