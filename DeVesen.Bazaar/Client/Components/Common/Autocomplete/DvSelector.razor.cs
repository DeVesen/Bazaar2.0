using MudBlazor;

namespace DeVesen.Bazaar.Client.Components.Common.Autocomplete
{
    public partial class DvSelector : MudFormComponent<DvSelector.SelectorItemBase, string>
    {
        public DvSelector() : base(new SelectorConverter())
        {
            
        }


        public record SelectorItemBase
        {
            public required string Caption { get; init; }
        }
        public record SelectorItem : SelectorItemBase;
        public record SelectorItemNew : SelectorItemBase;

        public class SelectorConverter : MudBlazor.Converter<SelectorItemBase, string>
        {
            public SelectorConverter()
            {
                SetFunc = ToString;
                GetFunc = ToElement;
            }

            public static string? ToString(SelectorItemBase? arg)
            {
                return arg?.ToString();
            }

            public static SelectorItemBase? ToElement(string? arg)
            {
                return string.IsNullOrWhiteSpace(arg)
                    ? null
                    : new SelectorItem { Caption = arg };
            }
        }
    }
}
