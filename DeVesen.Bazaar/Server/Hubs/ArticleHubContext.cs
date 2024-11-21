using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared.Events;
using Microsoft.AspNetCore.SignalR;

namespace DeVesen.Bazaar.Server.Hubs;

public class ArticleHubContext(IHubContext<ArticleHub> hubContext) : IDisposable
{
    private readonly EventBuffer<ArticleAddedArgs> _articleAddedBuffer = new(10, 1000, lst => hubContext.Clients.All.SendAsync("Added", lst));
    private readonly EventBuffer<ArticleUpdatedArgs> _articleUpdatedBuffer = new(10, 1000, lst => hubContext.Clients.All.SendAsync("Updated", lst));
    private readonly EventBuffer<ArticleRemovedArgs> _articleRemovedBuffer = new(10, 1000, lst => hubContext.Clients.All.SendAsync("Removed", lst));
    private readonly EventBuffer<ArticleStatusChangedArgs> _articleStatusChangedBuffer = new(10, 1000, lst => hubContext.Clients.All.SendAsync("StatusChanged", lst));

    public async Task SendAdded(ArticleEntity article)
    {
        _articleAddedBuffer.AddEvent(new ArticleAddedArgs(article.ToDomain().ToDto()));
        await Task.Delay(1);
    }

    public async Task SendUpdated(ArticleEntity article)
    {
        _articleUpdatedBuffer.AddEvent(new ArticleUpdatedArgs(article.ToDomain().ToDto()));
        await Task.Delay(1);
    }

    public async Task SendRemoved(ArticleEntity article)
    {
        _articleRemovedBuffer.AddEvent(new ArticleRemovedArgs(article.ToDomain().ToDto()));
        await Task.Delay(1);
    }


    public async Task SendApproved(ArticleEntity article)
    {
        _articleStatusChangedBuffer.AddEvent(new ArticleStatusChangedArgs(article.ToDomain().ToDto()));
        await Task.Delay(1);
    }

    public async Task SendSold(ArticleEntity article)
    {
        _articleStatusChangedBuffer.AddEvent(new ArticleStatusChangedArgs(article.ToDomain().ToDto()));
        await Task.Delay(1);
    }

    public async Task SendReturned(ArticleEntity article)
    {
        _articleStatusChangedBuffer.AddEvent(new ArticleStatusChangedArgs(article.ToDomain().ToDto()));
        await Task.Delay(1);
    }

    public async Task SendSettled(ArticleEntity article)
    {
        _articleStatusChangedBuffer.AddEvent(new ArticleStatusChangedArgs(article.ToDomain().ToDto()));
        await Task.Delay(1);
    }

    public void Dispose()
    {
        _articleAddedBuffer.Dispose();
        _articleUpdatedBuffer.Dispose();
        _articleRemovedBuffer.Dispose();
        _articleStatusChangedBuffer.Dispose();
    }
}