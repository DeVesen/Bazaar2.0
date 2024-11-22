using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Validator;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using DeVesen.Bazaar.Shared.Basics;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ManufacturerController(ManufacturerStorage manufacturerStorage) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<ManufacturerEntity>> GetAllAsync()
        => await manufacturerStorage.GetAllAsync();


    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] ManufacturerCreateDto dto)
    {
        var entity = ToEntity(dto);
        var result = await new ManufacturerValidator(true, manufacturerStorage).ValidateAsync(entity);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await manufacturerStorage.CreateAsync(entity);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, [FromBody] ManufacturerUpdateDto dto)
    {
        var entity = ToEntity(id, dto);
        var result = await new ManufacturerValidator(false, manufacturerStorage).ValidateAsync(entity);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await manufacturerStorage.UpdateAsync(entity);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        await manufacturerStorage.DeleteAsync(id);

        return Ok();
    }


    private static ManufacturerEntity ToEntity(ManufacturerCreateDto dto)
        => new()
        {
            Id = ToShortHash(dto),
            Name = dto.Name
        };

    private static ManufacturerEntity ToEntity(string id, ManufacturerUpdateDto dto)
        => new()
        {
            Id = id,
            Name = dto.Name
        };

    private static string ToShortHash(ManufacturerCreateDto dto)
    {
        var builder = new StringBuilder();

        builder.Append(dto.Name);
        builder.Append(Guid.NewGuid().ToString());
        builder.Append(DateTime.UtcNow.Ticks);

        return builder.ToString().ToShortHash();
    }
}