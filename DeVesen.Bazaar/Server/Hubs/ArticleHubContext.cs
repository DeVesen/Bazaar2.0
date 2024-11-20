using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Shared.Events;
using Microsoft.AspNetCore.SignalR;

namespace DeVesen.Bazaar.Server.Hubs;

public class ArticleHubContext(IHubContext<ArticleHub> hubContext) : IDisposable
{
    private readonly EventBuffer<ArticleAddedArgs> _articleAddedBuffer = new(10, 1000, lst => hubContext.Clients.All.SendAsync("Added", lst));
    private readonly EventBuffer<ArticleUpdatedArgs> _articleUpdatedBuffer = new(10, 1000, lst => hubContext.Clients.All.SendAsync("Updated", lst));
    private readonly EventBuffer<ArticleRemovedArgs> _articleRemovedBuffer = new(10, 1000, lst => hubContext.Clients.All.SendAsync("Removed", lst));
    private readonly EventBuffer<ArticleStatusChangedArgs> _articleStatusChangedBuffer = new(10, 1000, lst => hubContext.Clients.All.SendAsync("StatusChanged", lst));

    public async Task SendAdded(Article article)
    {
        _articleAddedBuffer.AddEvent(new ArticleAddedArgs(article.ToDto()));
        await Task.Delay(1);
    }

    public async Task SendUpdated(Article article)
    {
        _articleUpdatedBuffer.AddEvent(new ArticleUpdatedArgs(article.ToDto()));
        await Task.Delay(1);
    }

    public async Task SendRemoved(Article article)
    {
        _articleRemovedBuffer.AddEvent(new ArticleRemovedArgs(article.ToDto()));
        await Task.Delay(1);
    }


    public async Task SendApproved(Article article)
    {
        _articleStatusChangedBuffer.AddEvent(new ArticleStatusChangedArgs(article.ToDto()));
        await Task.Delay(1);
    }

    public async Task SendSold(Article article)
    {
        _articleStatusChangedBuffer.AddEvent(new ArticleStatusChangedArgs(article.ToDto()));
        await Task.Delay(1);
    }

    public async Task SendReturned(Article article)
    {
        _articleStatusChangedBuffer.AddEvent(new ArticleStatusChangedArgs(article.ToDto()));
        await Task.Delay(1);
    }

    public async Task SendSettled(Article article)
    {
        _articleStatusChangedBuffer.AddEvent(new ArticleStatusChangedArgs(article.ToDto()));
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

public class EventBuffer<T>
{
    private readonly List<T> _buffer = [];
    private readonly Timer _timer;
    private readonly object _lock = new();

    private readonly int _maxEventCount;
    private readonly int _timeoutMs;

    public Action<List<T>> ProcessEvents { get; set; }

    public EventBuffer(int maxEventCount, int timeoutMs, Action<List<T>> processEvents)
    {
        _maxEventCount = maxEventCount;
        _timeoutMs = timeoutMs;
        ProcessEvents = processEvents ?? throw new ArgumentNullException(nameof(processEvents));

        _timer = new Timer(OnTimeout, null, Timeout.Infinite, Timeout.Infinite);
    }

    public void AddEvent(T newEvent)
    {
        lock (_lock)
        {
            _buffer.Add(newEvent);

            if (_buffer.Count >= _maxEventCount)
            {
                ProcessBuffer();
            }
            else
            {
                // Timer neu starten
                _timer.Change(_timeoutMs, Timeout.Infinite);
            }
        }
    }

    private void OnTimeout(object state)
    {
        lock (_lock)
        {
            if (_buffer.Any())
            {
                ProcessBuffer();
            }
        }
    }

    private void ProcessBuffer()
    {
        var eventsToProcess = _buffer.Take(_maxEventCount).ToList();
        _buffer.RemoveRange(0, eventsToProcess.Count);

        ProcessEvents?.Invoke(eventsToProcess);

        // Timer stoppen, wenn keine Events mehr im Buffer sind
        if (_buffer.Count == 0)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        else
        {
            // Falls noch Events übrig sind, den Timer neu starten
            _timer.Change(_timeoutMs, Timeout.Infinite);
        }
    }

    public void Dispose()
    {
        lock (_lock)
        {
            _timer.Dispose();
        }
    }
}
