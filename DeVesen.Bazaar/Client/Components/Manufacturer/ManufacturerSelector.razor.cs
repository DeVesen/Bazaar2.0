using MudBlazor;

namespace DeVesen.Bazaar.Client.Components.Manufacturer
{
    public partial class ManufacturerSelector : MudFormComponent<string, string>
    {
        public ManufacturerSelector() : base(new DefaultConverter<string>())
        {

        }
    }
}
