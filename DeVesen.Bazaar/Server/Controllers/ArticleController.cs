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
public class ArticleController(ArticleStorage articleStorage, ArticleValidator articleValidator) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<ArticleDto>> GetAllAsync([FromQuery] ArticleFilter? parameters)
    {
        var elements = await articleStorage.GetAllAsync(parameters ?? new ArticleFilter());

        return elements.Select(p => p.ToDto());
    }

    [HttpPost("book-sale")]
    public async Task<ActionResult> BookOrderAsync([FromBody] SalesOrderDto dto)
    {
        var orderArticles = dto.Positions
            .Select(p => p.ToDomain())
            .ToArray();

        await articleStorage.BookOrderAsync(orderArticles);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] ArticleCreateDto dto)
    {
        var element = dto.ToDomain();
        var result = await articleValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(new FailedRequestMessage(result.Errors.First().ErrorMessage));
        }

        await articleStorage.CreateAsync(element);

        return Ok(new ArticleCreatedDto { Id = element.Id });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, [FromBody] ArticleUpdateDto dto)
    {
        if (await articleStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.Article.NotFoundById, _ => id));
        }

        var element = (id, dto).ToDomain();
        var result = await articleValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(new FailedRequestMessage(result.Errors.First().ErrorMessage));
        }

        await articleStorage.UpdateAsync(element);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        if (await articleStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(new FailedRequestMessage(ResourceText.Transform(ResourceText.Article.NotFoundById, _ => id)));
        }

        await articleStorage.DeleteByIdAsync(id);

        return Ok();
    }
}