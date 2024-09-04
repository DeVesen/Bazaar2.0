using MudBlazor;

namespace DeVesen.Bazaar.Client.Components.Common.TextField;

public partial class DvTextField<T> : MudTextField<T>
{
    public DvTextField()
    {
        ShrinkLabel = true;
        Margin = Margin.Dense;
        Variant = Variant.Outlined;
        DebounceInterval = 600;
    }
}
