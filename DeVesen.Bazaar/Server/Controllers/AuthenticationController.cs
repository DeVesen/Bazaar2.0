using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared;
using DeVesen.Bazaar.Shared.Basics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController(IVendorRepository vendorRepository) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var vendorResult = await TryGetVendor(loginModel.Username, loginModel.Password);
        if (vendorResult.Found is false)
        {
            return Unauthorized();
        }

        var token = GenerateJwtToken(vendorResult.Entity);

        return Ok(token);
    }

    private async Task<(bool Found, VendorEntity Entity)> TryGetVendor(string userName, string password)
    {
        var vendorElement = await GetAllVendors();
        var vendorEntity = vendorElement.FirstOrDefault(p =>
            $"{p.LastName}.{p.FirstName}".Equals(userName, StringComparison.OrdinalIgnoreCase));

        if (vendorEntity == null)
        {
            return (false, null!);
        }

        var vendorPassword = (vendorEntity.LastName + "-" + vendorEntity.Id).ToShortHash();
        return vendorPassword.Equals(password, StringComparison.OrdinalIgnoreCase)
            ? (true, vendorEntity)
            : (false, null!);
    }

    private async Task<IEnumerable<VendorEntity>> GetAllVendors()
    {
        var result = new List<VendorEntity> { GetMasterEntity() };

        result.AddRange(await vendorRepository.GetAllAsync());

        return result;
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

    private static VendorEntity GetMasterEntity()
        => new()
        {
            Id = "cc322bf4",
            FirstName = "Hochstadt",
            LastName = "Wintersport"
        };
}