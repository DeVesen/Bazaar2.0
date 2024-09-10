using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Shared.Services;

namespace DeVesen.Bazaar.Server.Services
{
    public class ArticleNumberService(ArticleStorage articleStorage, SystemClock systemClock) : IDisposable
    {
        private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
        private readonly List<(long Number, DateTime Created)> _lastRequestedNumbers = [];
        private bool _disposed;

        public async Task<long> GetNextNumber()
        {
            await _semaphoreSlim.WaitAsync();

            try
            {
                ClearLastRequested();

                for (long number = 1; number < long.MaxValue; number++)
                {
                    if (await IsNumberUsed(number))
                    {
                        continue;
                    }

                    _lastRequestedNumbers.Add((number, systemClock.GetNow()));

                    return number;
                }

                throw new NotImplementedException();
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private async Task<bool> IsNumberUsed(long number)
        {
            return IsNumberBlocked(number) ||
                   await articleStorage.ExistByNumberAsync(number);
        }

        private bool IsNumberBlocked(long number)
        {
            return _lastRequestedNumbers.Any(p => p.Number == number);
        }

        private void ClearLastRequested()
        {
            const double aliveMinutes = 10; // todo move to app-Config

            _lastRequestedNumbers.RemoveAll(p => IsOlderAs(p.Created, aliveMinutes));

            return;

            bool IsOlderAs(DateTime reference, double minutes)
            {
                var span = reference - systemClock.GetNow();
                return span.TotalMinutes > minutes;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing is false)
            {
                return;
            }

            _semaphoreSlim.Dispose();
            _disposed = true;
        }
    }
}
