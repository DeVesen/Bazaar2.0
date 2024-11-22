using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VendorController(VendorStorage vendorStorage, ArticleStorage articleStorage) : ControllerBase
{
    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var filter = new VendorFilter { Id = id };
        var items = await GetOverviewAsync(filter);
        var item = items.FirstOrDefault();

        if (item == null)
        {
            return NotFound($"Verkäufer '{id}' nicht bekannt!");
        }

        return Ok(item.Vendor);
    }


    [HttpGet("list")]
    public async Task<IEnumerable<VendorDto>> GetAllAsync([FromQuery] VendorFilter? parameters)
    {
        var elements = await vendorStorage.GetAllAsync(parameters ?? new VendorFilter());

        return elements.Select(p => p.ToDto());
    }


    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] VendorCreateDto dto)
    {
        var element = dto.ToDomain();

        await vendorStorage.CreateAsync(element);

        return Ok(element);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, [FromBody] VendorUpdateDto dto)
    {
        if (await vendorStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.Vendor.NotFoundById, _ => id));
        }

        var element = (id, dto).ToDomain();

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



    [HttpGet("overview")]
    public async Task<IList<VendorOverviewItemDto>> GetOverviewAsync([FromQuery] VendorFilter? parameters)
    {
        var vendors = (await vendorStorage.GetAllAsync(parameters ?? new VendorFilter())).ToArray();
        var articles = (await articleStorage.GetAllAsync()).ToArray();

        var vendorView =
            from vendor in vendors
            let vendorArticles = articles.Where(p => p.VendorId == vendor.Id).ToArray()
            select new { Vendor = vendor, Articles = vendorArticles };

        return vendorView.Select(vendor => new VendorOverviewItemDto
        {
            Vendor = vendor.Vendor.ToDto(),
            Counts = vendor.Articles.CalculateCounts(),
            Values = vendor.Articles.CalculateValues(vendor.Vendor.OfferUnitPrice, vendor.Vendor.SalesShare)
        }).ToList();
    }

    [HttpGet("{vendorId}/statistics")]
    public async Task<IActionResult> GetStatisticsByVendor(string vendorId)
    {
        if (await vendorStorage.ExistByIdAsync(vendorId) is false)
        {
            return NotFound();
        }

        var vendor = await vendorStorage.TryGetByIdAsync(vendorId);
        var articles = (await articleStorage.GetAllAsync(new ArticleFilter { VendorId = vendorId })).ToArray();

        var result = new VendorStatisticsDto
        {
            VendorId = vendorId,
            Counts = articles.CalculateCounts(),
            Values = articles.CalculateValues(vendor.Vendor.OfferUnitPrice, vendor.Vendor.SalesShare)
        };

        return Ok(result);
    }
}