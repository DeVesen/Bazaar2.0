using DeVesen.Bazaar.Shared.Events;
using Microsoft.AspNetCore.SignalR;

namespace DeVesen.Bazaar.Server.Hubs;

public class ArticleHubContext(IHubContext<ArticleHub> hubContext)
{
    public async Task SendAdded(string vendorId, string articleId, long articleNumber)
    {
        await hubContext.Clients.All.SendAsync("Added", new ArticleAddedArgs(vendorId, articleId, articleNumber));
    }

    public async Task SendUpdated(string vendorId, string articleId, long articleNumber)
    {
        await hubContext.Clients.All.SendAsync("Updated", new ArticleUpdatedArgs(vendorId, articleId, articleNumber));
    }

    public async Task SendRemoved(string vendorId, string articleId, long articleNumber)
    {
        await hubContext.Clients.All.SendAsync("Removed", new ArticleRemovedArgs(vendorId, articleId, articleNumber));
    }


    public async Task SendApproved(string vendorId, string articleId, long articleNumber)
    {
        await SendStatusChangedAsync(vendorId, articleId, articleNumber, ArticleStatusChangedInfo.ChangedField.Approved);
    }

    public async Task SendSold(string vendorId, string articleId, long articleNumber)
    {
        await SendStatusChangedAsync(vendorId, articleId, articleNumber, ArticleStatusChangedInfo.ChangedField.Sold);
    }

    public async Task SendReturned(string vendorId, string articleId, long articleNumber)
    {
        await SendStatusChangedAsync(vendorId, articleId, articleNumber, ArticleStatusChangedInfo.ChangedField.Returned);
    }

    public async Task SendSettled(string vendorId, string articleId, long articleNumber)
    {
        await SendStatusChangedAsync(vendorId, articleId, articleNumber, ArticleStatusChangedInfo.ChangedField.Settled);
    }

    private async Task SendStatusChangedAsync(string vendorId, string articleId, long articleNumber, ArticleStatusChangedInfo.ChangedField field)
    {
        await hubContext.Clients.All.SendAsync("StatusChanged", new ArticleStatusChangedInfo(vendorId, articleId, articleNumber, field));
    }
}