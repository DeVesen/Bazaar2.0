using DeVesen.Bazaar.Server.Controllers;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Tests.Fake.Repository;
using DeVesen.Bazaar.Server.Validator;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DeVesen.Bazaar.Server.Tests.Controller;

public class ArticleCategoryControllerTests
{

    [Fact]
    public async Task GetAllAsync_EmptyRepository_ExpectEmpty()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake();
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);

        // Act
        var result = await controller.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ContainedEntity_ExpectEntities()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1, Faker.Entity.ArticleCategory2);
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);
        var expected = new[] { Faker.Dto.ArticleCategory1, Faker.Dto.ArticleCategory2 };

        // Act
        var result = await controller.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }


    [Fact]
    public async Task CreateAsync_EmptyRepository_ExpectOneEntity()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake();
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);

        // Act
        var result = await controller.CreateAsync(Faker.Dto.ArticleCategoryCreate01);

        // Assert
        repository.InnerList.Count.Should().Be(1);

        result.Should().BeOfType<OkResult>();
        ((OkResult)result).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntity_ExpectTwoEntity()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);

        // Act
        var result = await controller.CreateAsync(Faker.Dto.ArticleCategoryCreate02);

        // Assert
        repository.InnerList.Count.Should().Be(2);

        result.Should().BeOfType<OkResult>();
        ((OkResult)result).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntityName_ExpectBadRequest()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);

        // Act
        var result = await controller.CreateAsync(Faker.Dto.GetArticleCategoryCreate(Faker.Entity.ArticleCategory1.Name));

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        ((BadRequestObjectResult)result).StatusCode.Should().Be(400);
        ((BadRequestObjectResult)result).Value.Should().Be("'Bezeichnung' ist bereits vergeben!");
    }


    [Fact]
    public async Task UpdateAsync_EmptyRepository_ExpectBadRequest()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake();
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);
        var dto = Faker.Dto.GetArticleCategoryUpdate("Update FooBar");

        // Act
        var result = await controller.UpdateAsync(Faker.Dto.ArticleCategory2.Id, dto);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
        ((NotFoundObjectResult)result).Value.Should().Be($"Article-Category '{Faker.Dto.ArticleCategory2.Id}' nicht gefunden!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedOtherEntity_ExpectBadRequest()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);
        var dto = Faker.Dto.GetArticleCategoryUpdate("Update FooBar");

        // Act
        var result = await controller.UpdateAsync(Faker.Dto.ArticleCategory2.Id, dto);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
        ((NotFoundObjectResult)result).Value.Should().Be($"Article-Category '{Faker.Dto.ArticleCategory2.Id}' nicht gefunden!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedOtherEntityWithName_ExpectBadRequest()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1, Faker.Entity.ArticleCategory2);
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);
        var dto = Faker.Dto.GetArticleCategoryUpdate(Faker.Entity.ArticleCategory1.Name);

        // Act
        var result = await controller.UpdateAsync(Faker.Entity.ArticleCategory2.Id, dto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        ((BadRequestObjectResult)result).StatusCode.Should().Be(400);
        ((BadRequestObjectResult)result).Value.Should().Be($"'Bezeichnung' ist bereits vergeben!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);
        var dto = Faker.Dto.GetArticleCategoryUpdate("Hello FooBar");

        // Act
        var result = await controller.UpdateAsync(Faker.Domain.ArticleCategory1.Id, dto);

        // Assert
        repository.InnerList[0].Id.Should().Be(Faker.Domain.ArticleCategory1.Id);
        repository.InnerList[0].Name.Should().Be(dto.Name);

        result.Should().BeOfType<OkResult>();
    }


    [Fact]
    public async Task DeleteAsync_EmptyRepository_ExpectBadRequest()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake();
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.ArticleCategory2.Id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
        ((NotFoundObjectResult)result).Value.Should().Be($"Article-Category '{Faker.Dto.ArticleCategory2.Id}' nicht gefunden!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedOtherEntity_ExpectBadRequest()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.ArticleCategory2.Id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
        ((NotFoundObjectResult)result).Value.Should().Be($"Article-Category '{Faker.Dto.ArticleCategory2.Id}' nicht gefunden!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
        var storage = new ArticleCategoryStorage(repository);
        var validator = new ArticleCategoryValidator(storage);
        var controller = new ArticleCategoryController(storage, validator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.ArticleCategory1.Id);

        // Assert
        result.Should().BeOfType<OkResult>();
    }
}