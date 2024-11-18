using DeVesen.Bazaar.Shared.Events;
using Microsoft.AspNetCore.SignalR;

namespace DeVesen.Bazaar.Server.Hubs;

public class VendorHubContext(IHubContext<VendorHub> hubContext)
{
    public void SendAdded(string vendorId)
    {
        Task.Run(async () =>
        {
            await hubContext.Clients.All.SendAsync("Added", new VendorAddedArgs(vendorId));
        });
    }

    public void SendUpdated(string vendorId, VendorUpdatedArgs.Reasons reason)
    {
        Task.Run(async () =>
        {
            await hubContext.Clients.All.SendAsync("Updated", new VendorUpdatedArgs(vendorId, reason));
        });
    }

    public void SendRemoved(string vendorId)
    {
        Task.Run(async () =>
        {
            await hubContext.Clients.All.SendAsync("Removed", new VendorRemovedArgs(vendorId));
        });
    }
}