using DeVesen.Bazaar.Server.Infrastructure;
using Microsoft.AspNetCore.SignalR;

namespace DeVesen.Bazaar.Server.Hubs;

public class ManufacturerHubContext(IHubContext<ManufacturerHub> hubContext)
{
    public async Task SendAdded(ManufacturerEntity entity)
    {
        await hubContext.Clients.All.SendAsync("Added", entity);
    }

    public async Task SendUpdated(ManufacturerEntity entity)
    {
        await hubContext.Clients.All.SendAsync("Updated", entity);
    }

    public async Task SendRemoved(string id)
    {
        await hubContext.Clients.All.SendAsync("Removed", id);
    }
}