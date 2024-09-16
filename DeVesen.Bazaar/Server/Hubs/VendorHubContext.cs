using Microsoft.AspNetCore.SignalR;

namespace DeVesen.Bazaar.Server.Hubs;

public class VendorHubContext(IHubContext<VendorHub> hubContext)
{
    public async Task SendAdded()
    {
        await hubContext.Clients.All.SendAsync("Added");
    }

    public async Task SendUpdated(string vendorId)
    {
        await hubContext.Clients.All.SendAsync("Updated", vendorId);
    }

    public async Task SendRemoved(string vendorId)
    {
        await hubContext.Clients.All.SendAsync("Removed", vendorId);
    }
}