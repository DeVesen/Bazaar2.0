using MudBlazor.Interfaces;

namespace DeVesen.Bazaar.Client.Components.Common;

public class CtrlEventArgs<T> : EventArgs
{
    public IFormComponent Sender { get; }

    public T Args { get; }

    public CtrlEventArgs(IFormComponent sender, T args)
    {
        Sender = sender;
        Args = args;
    }
}