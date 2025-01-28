using DeVesen.Bazaar.Client.Extensions;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

public class ManufacturerFacade(IDispatcher dispatcher, ManufacturerHubConnectionService manufacturerHub)
{
    public void Fetch(string? name = null, string? searchText = null)
        => dispatcher.Dispatch(new ManufacturerActions.Fetch(name, searchText));


    public async Task StartCallbacks()
    {
        if (manufacturerHub.IsWorking())
        {
            return;
        }

        await manufacturerHub.StartAsync();

        manufacturerHub.RegisterOnAdded(OnAdded);
        manufacturerHub.RegisterOnUpdated(OnUpdated);
        manufacturerHub.RegisterOnRemoved(OnRemoved);
    }


    public async Task StopCallbacks()
    {
        await manufacturerHub.StopAsync();
    }


    private void OnAdded(Models.Manufacturer element)
    {
        dispatcher.Dispatch(new ManufacturerActions.AppendElement(element));
    }

    private void OnUpdated(Models.Manufacturer element)
    {
        dispatcher.Dispatch(new ManufacturerActions.UpdateElement(element));
    }

    private void OnRemoved(string id)
    {
        dispatcher.Dispatch(new ManufacturerActions.RemoveElement(id));
    }
}