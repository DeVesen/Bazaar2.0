using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Domain;
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
    private readonly ArticleStorage _articleStorage;
    private readonly VendorValidator _vendorValidator;

    public VendorController(VendorStorage vendorStorage,
                            ArticleStorage articleStorage,
                            VendorValidator vendorValidator)
    {
        _vendorStorage = vendorStorage;
        _articleStorage = articleStorage;
        _vendorValidator = vendorValidator;
    }

    [HttpGet]
    public async Task<IEnumerable<VendorViewDto>> GetAllAsync()
    {
        var allVendors = (await _vendorStorage.GetAllAsync()).ToArray();
        var allVendorArticleStats = (await _articleStorage.GetStatisticPerVendor()).ToArray();

        var groupedItems = from vendor in allVendors
                           join statistic in allVendorArticleStats on vendor.Id equals statistic.VendorId into gj
            from subVendor2 in gj.DefaultIfEmpty(new VendorArticleStatistic{VendorId = vendor.Id})
            select new VendorViewDto
            {
                Item = vendor.ToDto(),
                Statistic = subVendor2.ToDto()
            };

        return groupedItems;
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

        return Ok(element);
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