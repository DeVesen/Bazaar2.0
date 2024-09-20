using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Shared;

[ExcludeFromCodeCoverage]
public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}