using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Validator;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ArticleCategoryController : ControllerBase
{
    private readonly ArticleCategoryStorage _articleCategoryStorage;
    private readonly ArticleCategoryValidator _articleCategoryValidator;

    public ArticleCategoryController(ArticleCategoryStorage articleCategoryStorage,
                                     ArticleCategoryValidator articleCategoryValidator)
    {
        _articleCategoryStorage = articleCategoryStorage;
        _articleCategoryValidator = articleCategoryValidator;
    }

    [HttpGet]
    public async Task<IEnumerable<ArticleCategoryDto>> GetAllAsync()
    {
        var elements = await _articleCategoryStorage.GetAllAsync();
        return elements.Select(p => p.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] ArticleCategoryCreateDto dto)
    {
        var element = dto.ToDomain();
        var result = await _articleCategoryValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await _articleCategoryStorage.CreateAsync(element);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, [FromBody] ArticleCategoryUpdateDto dto)
    {
        if (await _articleCategoryStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.ArticleCategory.NotFoundById, _ => id));
        }

        var element = (id, dto).ToDomain();
        var result = await _articleCategoryValidator.ValidateAsync(element);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await _articleCategoryStorage.UpdateAsync(element);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        if (await _articleCategoryStorage.ExistByIdAsync(id) is false)
        {
            return NotFound(ResourceText.Transform(ResourceText.ArticleCategory.NotFoundById, _ => id));
        }

        await _articleCategoryStorage.DeleteAsync(id);

        return Ok();
    }
}