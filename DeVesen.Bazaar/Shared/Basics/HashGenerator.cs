using System.Security.Cryptography;
using System.Text;

namespace DeVesen.Bazaar.Shared.Basics;

public static class HashGenerator
{
    public static string ToShortHash(this string data)
    {
        using var sha256Hash = SHA256.Create();
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString().Substring(0, 8);
    }
}