using MudBlazor;

namespace SmartBazaar.Client.Shared.Components.DvAutocompleteSelect
{
    public partial class DvAutocompleteSelect : MudBaseInput<DvAutocompleteSelect.SelectorItem>
    {
        public DvAutocompleteSelect()
        {
            Converter = new SelectorConverter();
            ShrinkLabel = true;
        }


        public class SelectorItemBase
        {
            public required string Caption { get; init; }

            public string? ImgSrc { get; set; }
        }

        public class SelectorItem : SelectorItemBase;

        public class SelectorItemNew : SelectorItemBase;


        public class SelectorConverter : MudBlazor.Converter<SelectorItem, string>
        {
            public SelectorConverter()
            {
                SetFunc = ToString;
                GetFunc = ToElement;
            }

            public static string? ToString(SelectorItemBase? arg)
            {
                return arg?.Caption;
            }

            public static SelectorItem? ToElement(string? arg)
            {
                return string.IsNullOrWhiteSpace(arg)
                    ? null
                    : new SelectorItem { Caption = arg };
            }
        }
    }
}
