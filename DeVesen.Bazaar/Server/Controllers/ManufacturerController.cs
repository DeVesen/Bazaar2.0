using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Validator;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ManufacturerController : ControllerBase
{
    private readonly ManufacturerStorage _manufacturerStorage;
    private readonly ManufacturerValidator _manufacturerValidator;

    public ManufacturerController(ManufacturerStorage manufacturerStorage,
                                  ManufacturerValidator manufacturerValidator)
    {
        _manufacturerStorage = manufacturerStorage;
        _manufacturerValidator = manufacturerValidator;
    }

    [HttpGet]
    public async Task<IEnumerable<ManufacturerDto>> GetAllAsync()
    {
        var elements = await _manufacturerStorage.GetAllAsync();
        return elements.Select(p => p.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] ManufacturerCreateDto dto)
    {
        var element = (Guid.NewGuid(), dto).ToDomain();
        var result = await _manufacturerValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await _manufacturerStorage.CreateAsync(element);

        return Ok();
    }

    [HttpPut("{idStr}")]
    public async Task<ActionResult> UpdateAsync(string idStr, [FromBody] ManufacturerUpdateDto dto)
    {
        if (Guid.TryParse(idStr, out var itemId) is false)
        {
            return BadRequest("ID is not in valid format!");
        }

        if (await _manufacturerStorage.ExistByIdAsync(itemId) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.Manufacturer.NotFoundById, _ => idStr));
        }

        var element = (itemId, dto).ToDomain();
        var result = await _manufacturerValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await _manufacturerStorage.UpdateAsync(element);

        return Ok();
    }

    [HttpDelete("{idStr}")]
    public async Task<ActionResult> DeleteAsync(string idStr)
    {
        if (Guid.TryParse(idStr, out var itemId) is false)
        {
            return BadRequest("ID is not in valid format!");
        }

        if (await _manufacturerStorage.ExistByIdAsync(itemId) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.Manufacturer.NotFoundById, _ => idStr));
        }

        await _manufacturerStorage.DeleteAsync(itemId);

        return Ok();
    }
}