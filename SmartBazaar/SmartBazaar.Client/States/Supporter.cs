namespace SmartBazaar.Client.States;

public static class Supporter
{
    public static async Task WaitUntil(Func<bool> condition, int timeoutMs = 800)
    {
        const int delay = 100;

        while (condition() is false && timeoutMs >= 0)
        {
            await Task.Delay(delay);
            timeoutMs -= delay;
        }

        if (timeoutMs <= 0)
        {
            throw new TimeoutException();
        }
    }
}