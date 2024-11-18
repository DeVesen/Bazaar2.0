namespace DeVesen.Bazaar.Shared.Events;

public record VendorUpdatedArgs(string Id, VendorUpdatedArgs.Reasons Reason)
{
    public enum Reasons
    {
        MasterData,
        ArticleData
    }
}