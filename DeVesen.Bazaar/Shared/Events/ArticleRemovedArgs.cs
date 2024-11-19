namespace DeVesen.Bazaar.Shared.Events;

public record ArticleRemovedArgs(string VendorId, string ArticleId, long ArticleNumber);