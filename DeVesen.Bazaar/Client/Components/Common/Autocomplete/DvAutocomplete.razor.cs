using MudBlazor;
using System.Net;

namespace DeVesen.Bazaar.Client.Components.Common.Autocomplete;

public partial class DvAutocomplete<T> : MudAutocomplete<T>
{
    public DvAutocomplete()
    {
        Dense = true;
        ShrinkLabel = true;
        IconSize = Size.Small;
        Margin = Margin.Dense;
        Variant = Variant.Outlined;
        DebounceInterval = 600;
        ResetValueOnEmptyText = true;
        CoerceText = true;
    }
}
