using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Validator;
using DeVesen.Bazaar.Shared;
using DeVesen.Bazaar.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly SystemClock _systemClock;
    private readonly ArticleStorage _articleStorage;
    private readonly ArticleValidator _articleValidator;

    public ArticleController(SystemClock systemClock,
                             ArticleStorage articleStorage,
                             ArticleValidator articleValidator)
    {
        _systemClock = systemClock;
        _articleStorage = articleStorage;
        _articleValidator = articleValidator;
    }

    [HttpGet]
    public async Task<IEnumerable<ArticleDto>> GetAllAsync([FromQuery] ArticleFilter? parameters)
    {
        var elements = await _articleStorage.GetAllAsync(parameters ?? new ArticleFilter());

        return elements.Select(p => p.ToDto());
    }

    [HttpPost("{number:long}/approve")]
    public async Task<ActionResult> ApproveAsync(long number)
    {
        if (await _articleStorage.ExistByNumberAsync(number) is false)
        {
            return BadRequest(new FailedRequestMessage($"Artikel {number} nicht gefunden!"));
        }

        var article = await _articleStorage.GetByNumberAsync(number);

        if (article.ApprovedForSale.HasValue)
        {
            return Ok();
        }

        article.ApprovedForSale = _systemClock.GetNow();

        await _articleStorage.UpdateAsync(article);

        return Ok();
    }

    [HttpPost("book-sale")]
    public async Task<ActionResult> BookOrderAsync([FromBody] SalesOrderDto dto)
    {
        var orderArticles = dto.Positions
            .Select(p => p.ToDomain())
            .ToArray();

        await _articleStorage.BookOrderAsync(orderArticles);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] ArticleCreateDto dto)
    {
        var element = dto.ToDomain();
        var result = await _articleValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(new FailedRequestMessage(result.Errors.First().ErrorMessage));
        }

        await _articleStorage.CreateAsync(element);

        return Ok(new ArticleCreatedDto { Id = element.Id });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, [FromBody] ArticleUpdateDto dto)
    {
        if (await _articleStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.Article.NotFoundById, _ => id));
        }

        var element = (id, dto).ToDomain();
        var result = await _articleValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(new FailedRequestMessage(result.Errors.First().ErrorMessage));
        }

        await _articleStorage.UpdateAsync(element);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        if (await _articleStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(new FailedRequestMessage(ResourceText.Transform(ResourceText.Article.NotFoundById, _ => id)));
        }

        await _articleStorage.DeleteByIdAsync(id);

        return Ok();
    }
}