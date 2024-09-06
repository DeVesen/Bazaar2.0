using DeVesen.Bazaar.Server.Controllers;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Tests.Fake.Repository;
using DeVesen.Bazaar.Server.Validator;
using DeVesen.Bazaar.Shared;
using DeVesen.Bazaar.Shared.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DeVesen.Bazaar.Server.Tests.Controller;

public class ArticleControllerTests
{
    private readonly SystemClock _systemClock;

    public ArticleControllerTests()
    {
        _systemClock = Mock.Of<SystemClock>();

        Mock.Get(_systemClock)
            .Setup(x => x.GetNow())
            .Returns(new DateTime(2024, 8, 1, 1, 1, 1));
    }

    [Fact]
    public async Task GetAllAsync_EmptyRepository_ExpectEmpty()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);

        // Act
        var result = await controller.GetAllAsync(null);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ContainedEntity_ExpectEntities()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1, Faker.Entity.Article2, Faker.Entity.Article3);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);
        var expected = new[] { Faker.Entity.Article1, Faker.Entity.Article2, Faker.Entity.Article3 };

        // Act
        var result = await controller.GetAllAsync(null);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }


    [Fact]
    public async Task CreateAsync_EmptyRepository_ExpectOneEntity()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor2);
        var articleRepository = new ArticleRepositoryFake();
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);

        // Act
        var result = await controller.CreateAsync(Faker.Dto.ArticleCreate02);

        // Assert
        articleRepository.InnerList.Count.Should().Be(1);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task CreateAsync_ContainedEntity_ExpectTwoEntity()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor2);
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);

        // Act
        var result = await controller.CreateAsync(Faker.Dto.ArticleCreate02);

        // Assert
        articleRepository.InnerList.Count.Should().Be(2);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task CreateAsync_ContainedEntityNumber_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);

        // Act
        var result = await controller.CreateAsync(Faker.Dto.ArticleCreate01);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        ((BadRequestObjectResult)result).Value.Should().Be(new FailedRequestMessage("'Nummer' ist bereits vergeben!"));
    }


    [Fact]
    public async Task UpdateAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);

        // Act
        var result = await controller.UpdateAsync(Faker.Entity.Article2.Id, Faker.Dto.ArticleUpdate02);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).Value.Should().Be($"Artikel '{Faker.Entity.Article2.Id}' nicht gefunden!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);

        // Act
        var result = await controller.UpdateAsync(Faker.Entity.Article2.Id, Faker.Dto.ArticleUpdate02);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).Value.Should().Be($"Artikel '{Faker.Entity.Article2.Id}' nicht gefunden!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);
        var article = Faker.Dto.GetArticleUpdate(vendorId: Faker.Entity.Vendor1.Id, title: "Hello Article");

        // Act
        var result = await controller.UpdateAsync(Faker.Entity.Article1.Id, article);

        // Assert
        articleRepository.InnerList[0].Id.Should().Be(Faker.Entity.Article1.Id);
        articleRepository.InnerList[0].Title.Should().Be(article.Title);

        result.Should().BeOfType<OkResult>();
    }


    [Fact]
    public async Task DeleteAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.Article2.Id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).Value.Should().Be(new FailedRequestMessage($"Artikel '{Faker.Entity.Article2.Id}' nicht gefunden!"));
    }

    [Fact]
    public async Task DeleteAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.Article2.Id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).Value.Should().Be(new FailedRequestMessage($"Artikel '{Faker.Entity.Article2.Id}' nicht gefunden!"));
    }

    [Fact]
    public async Task DeleteAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.Article1.Id);

        // Assert
        result.Should().BeOfType<OkResult>();
    }
}