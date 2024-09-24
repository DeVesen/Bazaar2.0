using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared;
using DeVesen.Bazaar.Shared.Basics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        if (TryGetVendor(loginModel.Username, loginModel.Password, out var vendorEntity) is false)
        {
            return Unauthorized();
        }

        var token = GenerateJwtToken(vendorEntity);

        return Ok(token);
    }

    private static bool TryGetVendor(string userName, string password, out VendorEntity entity)
    {
        var adminEntity = new VendorEntity
        {
            Id = "cc322bf4",
            FirstName = "Hochstadt",
            LastName = "Wintersport"
        };

        var adminUserName = $"{adminEntity.LastName}.{adminEntity.FirstName}";
        var adminPassword = (adminEntity.LastName + "-" + adminEntity.Id).ToShortHash();

        if (adminUserName.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
            adminPassword.Equals(password, StringComparison.Ordinal))
        {
            entity = adminEntity;

            return true;
        }

        entity = null!;

        return false;
    }

    private static string GenerateJwtToken(VendorEntity vendorEntity)
    {
        const string key =
            "QTA0NjMwMjEtMDY3RTQ0MEQ4MDZDOEQ0MzU0RDQxN0RGIHwgMkY0N0JBMzE4NEUwNDgyREFEOThERTY4OUZEQTdFREYgfCA3Q0NEMENGQTBBQTY0ODAwODkzQkJCQjU2OTA0ODE1Nw==";

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, vendorEntity.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.FamilyName, vendorEntity.LastName),
            new Claim(JwtRegisteredClaimNames.GivenName, vendorEntity.FirstName)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-audience",
            claims: claims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}