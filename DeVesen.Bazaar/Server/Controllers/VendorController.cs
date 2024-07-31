using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Validator;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VendorController : ControllerBase
{
    private readonly VendorStorage _vendorStorage;
    private readonly VendorValidator _vendorValidator;

    public VendorController(VendorStorage vendorStorage,
                            VendorValidator vendorValidator)
    {
        _vendorStorage = vendorStorage;
        _vendorValidator = vendorValidator;
    }

    [HttpGet]
    public async Task<IEnumerable<VendorDto>> GetAllAsync()
    {
        var elements = await _vendorStorage.GetAllAsync();
        return elements.Select(p => p.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] VendorCreateDto dto)
    {
        var element = dto.ToDomain();
        var result = await _vendorValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await _vendorStorage.CreateAsync(element);

        return Ok(new VendorCreatedDto { Id = element.Id });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, [FromBody] VendorUpdateDto dto)
    {
        if (await _vendorStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.Vendor.NotFoundById, _ => id));
        }

        var element = (id, dto).ToDomain();
        var result = await _vendorValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await _vendorStorage.UpdateAsync(element);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        if (await _vendorStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.Vendor.NotFoundById, _ => id));
        }

        await _vendorStorage.DeleteAsync(id);

        return Ok();
    }
}