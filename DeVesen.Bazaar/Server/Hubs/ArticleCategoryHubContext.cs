using Microsoft.AspNetCore.SignalR;

namespace DeVesen.Bazaar.Server.Hubs;

public class ArticleCategoryHubContext(IHubContext<ArticleCategoryHub> hubContext)
{
    public async Task SendAdded()
    {
        await hubContext.Clients.All.SendAsync("Added");
    }

    public async Task SendUpdated(string articleCategoryId)
    {
        await hubContext.Clients.All.SendAsync("Updated", articleCategoryId);
    }

    public async Task SendRemoved(string articleCategoryId)
    {
        await hubContext.Clients.All.SendAsync("Removed", articleCategoryId);
    }
}