using Fluxor;
using Microsoft.AspNetCore.Components;

namespace DeVesen.Bazaar.Client.Components.Common;

public partial class DispatchedAction : ComponentBase, IDisposable
{
    private bool _disposed;

    [Parameter]
    public required EventCallback<object> Callback { get; set; }

    protected override void OnInitialized()
    {
        Dispatcher.ActionDispatched += DispatcherOnActionDispatched;
    }

    private void DispatcherOnActionDispatched(object? sender, ActionDispatchedEventArgs e)
    {
        Callback.InvokeAsync(e.Action);
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        Dispose(true);
        GC.SuppressFinalize(this);
        _disposed = true;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Dispatcher.ActionDispatched -= DispatcherOnActionDispatched;
        }
    }
}