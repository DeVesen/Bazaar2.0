namespace SmartBazaar.Client.Shared.Components.DvSimpleList;

public partial class DvSimpleList
{
    public record Element
    {
        public required string Id { get; init; }

        public required string Title { get; init; }

        public object? Tag { get; set; }

        public string? ImgSrc { get; set; }
    }
}