namespace DeVesen.Bazaar.Client.Models;

public record VendorOverviewItem
{
    public required Vendor MasterData { get; set; }
    public required Statistics Stats { get; set; }

    public record Statistics
    {
        public long Open { get; set; }
        public long Sold { get; set; }
        public long Settled { get; set; }

        public override string ToString()
        {
            return $"{Open} / {Settled} / {Settled}";
        }
    }

    public bool Contains(string text)
    {
        return MasterData.Contains(text);
    }
}