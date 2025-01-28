using DeVesen.Bazaar.Server.Infrastructure;
using Microsoft.AspNetCore.SignalR;

namespace DeVesen.Bazaar.Server.Hubs;

public class ArticleCategoryHubContext(IHubContext<ArticleCategoryHub> hubContext)
{
    public async Task SendAdded(ArticleCategoryEntity entity)
    {
        await hubContext.Clients.All.SendAsync("Added", entity);
    }

    public async Task SendUpdated(ArticleCategoryEntity entity)
    {
        await hubContext.Clients.All.SendAsync("Updated", entity);
    }

    public async Task SendRemoved(string id)
    {
        await hubContext.Clients.All.SendAsync("Removed", id);
    }
}