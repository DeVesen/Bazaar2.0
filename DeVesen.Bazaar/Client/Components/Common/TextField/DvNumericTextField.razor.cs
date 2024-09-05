using MudBlazor;

namespace DeVesen.Bazaar.Client.Components.TextField;

public partial class DvNumericTextField<T> : MudNumericField<T>
{
    public DvNumericTextField()
    {
        ShrinkLabel = true;
        Margin = Margin.Dense;
        Variant = Variant.Outlined;
        DebounceInterval = 750;
        HideSpinButtons = true;
    }
}
