namespace DeVesen.Bazaar.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool BiContainsIgnoreCase(this object? a, object? b)
        {
            var aStr = a?.ToString();
            var bStr = b?.ToString();

            if (string.IsNullOrWhiteSpace(aStr) || string.IsNullOrWhiteSpace(bStr))
            {
                return false;
            }

            var aVar = aStr.ToLower();
            var bVar = bStr.ToLower();

            return aVar.Contains(bVar) || bVar.Contains(aVar);
        }
    }
}
