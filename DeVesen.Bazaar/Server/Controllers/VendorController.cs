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
    public async Task<IList<VendorOverviewItemDto>> GetAllAsync([FromQuery] VendorFilter? parameters)
    {
        var vendors = (await vendorStorage.GetAllAsync(parameters ?? new VendorFilter())).ToArray();
        var articles = (await articleStorage.GetAllAsync()).ToArray();

        var vendorView =
            from vendor in vendors
            let vendorArticles = articles.Where(p => p.VendorId == vendor.Id).ToArray()
            select new { Vendor = vendor, Articles = vendorArticles };

        return vendorView.Select(vendor =>
        {
            var articleStock = new VendorArticleStockDto
            {
                Recorded = vendor.Articles.Length,
                OnSale = vendor.Articles.Count(p => p.IsOnSale()),
                Sold = vendor.Articles.Count(p => p.IsSold()),
                Returned = vendor.Articles.Count(p => p.IsReturned()),
                Settled = vendor.Articles.Count(p => p.IsSettled())
            };

            return new VendorOverviewItemDto
            {
                Vendor = vendor.Vendor.ToDto(),
                ArticleStock = articleStock
            };
        }).ToList();
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
}