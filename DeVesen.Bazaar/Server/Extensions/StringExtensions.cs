namespace DeVesen.Bazaar.Server.Extensions
{
    public static class StringExtensions
    {
        public static bool BiContainsIgnoreCase(this string? a, string? b)
        {
            if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b))
            {
                return false;
            }

            var aVar = a.ToLower();
            var bVar = b.ToLower();

            return aVar.Contains(bVar) || bVar.Contains(aVar);
        }
    }
}
