namespace DeVesen.Bazaar.Shared.Events;

public record ArticleAddedArgs(string VendorId, string ArticleId, long ArticleNumber);