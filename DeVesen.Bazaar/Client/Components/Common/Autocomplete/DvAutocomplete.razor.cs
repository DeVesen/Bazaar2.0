using MudBlazor;

namespace DeVesen.Bazaar.Client.Components.Autocomplete;

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
