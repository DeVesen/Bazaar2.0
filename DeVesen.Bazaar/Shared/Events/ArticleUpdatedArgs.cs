namespace DeVesen.Bazaar.Shared.Events;

public record ArticleUpdatedArgs(string VendorId, string ArticleId, long ArticleNumber);