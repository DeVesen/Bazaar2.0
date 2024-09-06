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
public class VendorController(VendorStorage vendorStorage, ArticleStorage articleStorage, VendorValidator vendorValidator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var filter = new VendorFilter { Id = id };
        var items = await GetAllAsync(filter);
        var item = items.FirstOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpGet]
    public async Task<IEnumerable<VendorViewDto>> GetAllAsync([FromQuery] VendorFilter? parameters)
    {
        var allVendors = (await vendorStorage.GetAllAsync(parameters ?? new VendorFilter())).ToArray();
        var allVendorArticleStats = (await articleStorage.GetStatisticPerVendor()).ToArray();

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
        var result = await vendorValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await vendorStorage.CreateAsync(element);

        return Ok(element);
    }

    [HttpPost("{vendorId}/approve/{number:long}")]
    public async Task<ActionResult> ApproveArticleAsync(string vendorId, long number)
    {
        var result = await articleStorage.ApproveArticleAsync(number, vendorId);

        return result.IsSuccess
            ? Ok()
            : BadRequest(new FailedRequestMessage(result.Errors.First().Message));
    }

    [HttpPost("{vendorId}/giveback/{number:long}")]
    public async Task<ActionResult> GiveBackArticleAsync(string vendorId, long number)
    {
        var result = await articleStorage.GiveBackArticleAsync(number, vendorId);

        return result.IsSuccess
            ? Ok()
            : BadRequest(new FailedRequestMessage(result.Errors.First().Message));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, [FromBody] VendorUpdateDto dto)
    {
        if (await vendorStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.Vendor.NotFoundById, _ => id));
        }

        var element = (id, dto).ToDomain();
        var result = await vendorValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await vendorStorage.UpdateAsync(element);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        if (await vendorStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.Vendor.NotFoundById, _ => id));
        }

        await vendorStorage.DeleteAsync(id);

        return Ok();
    }
}