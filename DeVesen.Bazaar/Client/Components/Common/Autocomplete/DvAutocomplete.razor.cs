using MudBlazor;

namespace DeVesen.Bazaar.Client.Components.Common.Autocomplete;

public partial class DvAutocomplete<T> : MudAutocomplete<T>
{
    public DvAutocomplete()
    {
        ShrinkLabel = true;
        Variant = Variant.Filled;
        DebounceInterval = 600;
        ResetValueOnEmptyText = true;
        CoerceText = true;
    }
}
