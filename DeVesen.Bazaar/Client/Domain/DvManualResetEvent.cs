namespace DeVesen.Bazaar.Client.Domain
{
    public class DvManualResetEvent : IDisposable
    {
        private readonly ManualResetEvent _innerResetEvent;

        public DvManualResetEvent(bool initialState)
        {
            _innerResetEvent = new ManualResetEvent(initialState);
        }

        public object? Value { get; private set; }

        public bool Set() => Set(null);

        public bool Set(object? value)
        {
            Value = value;
            return _innerResetEvent.Set();
        }

        public bool Reset()
        {
            Value = null;
            return _innerResetEvent.Reset();
        }

        public bool WaitOne() => _innerResetEvent.WaitOne();

        public bool WaitOne(TimeSpan timeout) => _innerResetEvent.WaitOne(timeout);

        public bool WaitOne(int millisecondsTimeout, bool exitContext) => _innerResetEvent.WaitOne(millisecondsTimeout, exitContext);

        public bool WaitOne(TimeSpan timeout, bool exitContext) => _innerResetEvent.WaitOne(timeout, exitContext);

        public void Dispose()
        {
            _innerResetEvent.Dispose();
        }
    }
}
