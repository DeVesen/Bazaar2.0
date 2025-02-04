namespace SmartBazaar.Client.States;

public static class Supporter
{
    public static void WaitUntil(Func<bool> condition, int timeoutMs = 800)
    {
        const int delay = 100;

        while (condition() is false && timeoutMs >= 0)
        {
            Task.Delay(delay).Wait();
            timeoutMs -= delay;
        }

        if (timeoutMs <= 0)
        {
            throw new TimeoutException();
        }
    }
}