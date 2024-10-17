using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SettlementController(VendorStorage vendorStorage, ArticleStorage articleStorage) : ControllerBase
{
    [HttpGet("{vendorId}")]
    public async Task<VendorSettlementDto> GetVendorBilling(string vendorId)
    {
        var vendor = await vendorStorage.GetAsync(vendorId);
        var articleFilter = new ArticleFilter { VendorId = vendorId };
        var articles = (await articleStorage.GetAllAsync(articleFilter)).ToArray();

        var articleStock = new VendorArticleStockDto
        {
            Recorded = articles.Length,
            OnSale = articles.Count(p => p.IsOnSale()),
            Sold = articles.Count(p => p.IsSold()),
            Returned = articles.Count(p => p.IsReturned()),
            Settled = articles.Count(p => p.IsSettled())
        };

        var articleValue = new VendorArticleValueDto
        {
            TotalArticleValue = articles.Sum(p => p.Price01),
            TotalSalesValue = articles.Sum(p => p.SoldAt ?? 0.0d),
            TotalHandlingFee = articles.Count(p => p.ApprovedForSale.HasValue) * vendor.OfferUnitPrice,
            TotalSalesCommission = articles.Sum(p => p.SoldAt ?? 0.0d) * vendor.SalesShare
        };

        return new VendorSettlementDto
        {
            Vendor = vendor.ToDto(),
            ArticleStock = articleStock,
            ArticleValue = articleValue,
            Articles = articles.Select(p => p.ToDto())
        };
    }

    [HttpPost("{vendorId}/giveback/{number:long}")]
    public async Task<ActionResult> GiveBackArticleAsync(string vendorId, long number)
    {
        var result = await articleStorage.GiveBackArticleAsync(number, vendorId);

        return result.IsSuccess
            ? Ok()
            : BadRequest(new FailedRequestMessage(result.Errors.First().Message));
    }

    [HttpPut("{vendorId}/settle")]
    public async Task<ActionResult> SettleAsync(string vendorId)
    {
        var result = await articleStorage.SettleArticlesAsync(vendorId);

        return result.IsSuccess
            ? Ok()
            : BadRequest(new FailedRequestMessage(result.Errors.First().Message));
    }
}