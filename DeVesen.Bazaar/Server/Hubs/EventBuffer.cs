namespace DeVesen.Bazaar.Server.Hubs;

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