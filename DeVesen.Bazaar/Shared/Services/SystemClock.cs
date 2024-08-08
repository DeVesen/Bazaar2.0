namespace DeVesen.Bazaar.Shared.Services
{
    public class SystemClock
    {
        public virtual DateTime GetNow() => DateTime.Now;
    }
}
