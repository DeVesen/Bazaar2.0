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
        var element = dto.ToDomain();
        var result = await _manufacturerValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await _manufacturerStorage.CreateAsync(element);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, [FromBody] ManufacturerUpdateDto dto)
    {
        if (await _manufacturerStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.Manufacturer.NotFoundById, _ => id));
        }

        var element = (id, dto).ToDomain();
        var result = await _manufacturerValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await _manufacturerStorage.UpdateAsync(element);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        if (await _manufacturerStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.Manufacturer.NotFoundById, _ => id));
        }

        await _manufacturerStorage.DeleteAsync(id);

        return Ok();
    }
}