using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Services;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Shared;
using DeVesen.Bazaar.Shared.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VendorController(VendorStorage vendorStorage, ArticleStorage articleStorage, StatisticsService statisticsLogic) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var filter = new VendorFilter { Id = id };
        var items = await GetOverviewAsync(filter);
        var item = items.FirstOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
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
            Counts = statisticsLogic.CalculateCounts(vendor.Articles),
            Values = statisticsLogic.CalculateValues(vendor.Vendor.OfferUnitPrice, vendor.Vendor.SalesShare, vendor.Articles)
        }).ToList();
    }

    [HttpGet("{vendorId}/statistics")]
    public async Task<IActionResult> GetStatisticsByVendor(string vendorId)
    {
        if (await vendorStorage.ExistByIdAsync(vendorId) is false)
        {
            return NotFound();
        }

        var vendor = await vendorStorage.GetAsync(vendorId);
        var articles = (await articleStorage.GetAllAsync(new ArticleFilter { VendorId = vendorId })).ToArray();

        var result = new VendorStatisticsDto
        {
            VendorId = vendorId,
            Counts = statisticsLogic.CalculateCounts(articles),
            Values = statisticsLogic.CalculateValues(vendor.OfferUnitPrice, vendor.SalesShare, articles)
        };

        return Ok(result);
    }
}