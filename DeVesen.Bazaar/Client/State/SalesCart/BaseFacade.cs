using Fluxor;

namespace DeVesen.Bazaar.Client.State.SalesCart;

public class BaseFacade : IDisposable
{
    protected readonly IDispatcher Dispatcher;
    protected bool Disposed;

    protected BaseFacade(IDispatcher dispatcher)
    {
        Dispatcher = dispatcher;

        Dispatcher.ActionDispatched += DispatcherOnActionDispatched;
    }

    private void DispatcherOnActionDispatched(object? sender, ActionDispatchedEventArgs e)
    {
        OnActionDispatched(sender, e);
    }

    protected virtual void OnActionDispatched(object? sender, ActionDispatchedEventArgs e)
    {

    }

    public void Dispose()
    {
        if (Disposed)
        {
            return;
        }

        Disposed = true;

        Dispatcher.ActionDispatched -= DispatcherOnActionDispatched;

        DoDispose();
    }

    protected virtual void DoDispose()
    {

    }
}