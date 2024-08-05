using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Validator;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly ArticleStorage _articleStorage;
    private readonly ArticleValidator _articleValidator;

    public ArticleController(ArticleStorage articleStorage,
                            ArticleValidator articleValidator)
    {
        _articleStorage = articleStorage;
        _articleValidator = articleValidator;
    }

    [HttpGet]
    public async Task<IEnumerable<ArticleDto>> GetAllAsync([FromQuery] ArticleFilterDto? parameters)
    {
        var filter = parameters.ToDomain();
        var elements = await _articleStorage.GetAllAsync(filter);

        return elements.Select(p => p.ToDto());
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
            return BadRequest(new FailedRequestMessage(ResourceText.Transform(ResourceText.Article.NotFoundById, _ => id)));
        }

        await _articleStorage.DeleteByIdAsync(id);

        return Ok();
    }
}