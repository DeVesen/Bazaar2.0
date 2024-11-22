using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Validator;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using DeVesen.Bazaar.Shared.Basics;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ArticleCategoryController(ArticleCategoryStorage articleCategoryStorage) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<ArticleCategoryEntity>> GetAllAsync()
        => await articleCategoryStorage.GetAllAsync();


    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] ArticleCategoryCreateDto dto)
    {
        var entity = ToEntity(dto);
        var result = await new ArticleCategoryValidator(true, articleCategoryStorage).ValidateAsync(entity);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await articleCategoryStorage.CreateAsync(entity);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, [FromBody] ArticleCategoryUpdateDto dto)
    {
        var entity = ToEntity(id, dto);
        var result = await new ArticleCategoryValidator(false, articleCategoryStorage).ValidateAsync(entity);

        if (result.IsValid is false)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }

        await articleCategoryStorage.UpdateAsync(entity);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        await articleCategoryStorage.DeleteAsync(id);

        return Ok();
    }


    private static ArticleCategoryEntity ToEntity(ArticleCategoryCreateDto dto)
        => new()
        {
            Id = ToShortHash(dto),
            Name = dto.Name
        };

    private static ArticleCategoryEntity ToEntity(string id, ArticleCategoryUpdateDto dto)
        => new()
        {
            Id = id,
            Name = dto.Name
        };

    private static string ToShortHash(ArticleCategoryCreateDto dto)
    {
        var builder = new StringBuilder();

        builder.Append(dto.Name);
        builder.Append(Guid.NewGuid().ToString());
        builder.Append(DateTime.UtcNow.Ticks);

        return builder.ToString().ToShortHash();
    }
}