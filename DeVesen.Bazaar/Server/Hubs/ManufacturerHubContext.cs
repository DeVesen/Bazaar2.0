using Microsoft.AspNetCore.SignalR;

namespace DeVesen.Bazaar.Server.Hubs;

public class ManufacturerHubContext(IHubContext<ManufacturerHub> hubContext)
{
    public async Task SendAdded()
    {
        await hubContext.Clients.All.SendAsync("Added");
    }

    public async Task SendUpdated(string manufacturerId)
    {
        await hubContext.Clients.All.SendAsync("Updated", manufacturerId);
    }

    public async Task SendRemoved(string manufacturerId)
    {
        await hubContext.Clients.All.SendAsync("Removed", manufacturerId);
    }
}