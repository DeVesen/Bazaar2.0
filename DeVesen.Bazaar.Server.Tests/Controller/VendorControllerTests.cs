using DeVesen.Bazaar.Server.Controllers;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Tests.Fake.Repository;
using DeVesen.Bazaar.Server.Validator;
using DeVesen.Bazaar.Shared.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DeVesen.Bazaar.Server.Tests.Controller;

public class VendorControllerTests
{
    private readonly SystemClock _systemClock;

    public VendorControllerTests()
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
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);

        // Act
        var result = await controller.GetAllAsync(null);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ContainedEntity_ExpectEntities()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1, Faker.Entity.Vendor2, Faker.Entity.Vendor3);
        var articleRepository = new ArticleRepositoryFake();
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);

        // Act
        var result = await controller.GetAllAsync(null);

        // Assert
        result.Should().NotBeEmpty();
    }


    [Fact]
    public async Task CreateAsync_EmptyRepository_ExpectOneEntity()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);
        var dto = Faker.Dto.VendorCreate1;

        // Act
        var result = await controller.CreateAsync(dto);

        // Assert
        vendorRepository.InnerList.Count.Should().Be(1);

        result.Should().BeOfType<OkObjectResult>();
        ((OkObjectResult)result).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntity_ExpectTwoEntity()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);
        var dto = Faker.Dto.VendorCreate2;

        // Act
        var result = await controller.CreateAsync(dto);

        // Assert
        vendorRepository.InnerList.Count.Should().Be(2);

        result.Should().BeOfType<OkObjectResult>();
        ((OkObjectResult)result).StatusCode.Should().Be(200);
    }


    [Fact]
    public async Task UpdateAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);

        // Act
        var result = await controller.UpdateAsync(Faker.Domain.Vendor1.Id, Faker.Dto.VendorUpdate1);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task UpdateAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);

        // Act
        var result = await controller.UpdateAsync(Faker.Domain.Vendor2.Id, Faker.Dto.VendorUpdate1);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task UpdateAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);
        var vendor = Faker.Dto.GetVendorUpdate(firstName: "Hello Vendor");

        // Act
        var result = await controller.UpdateAsync(Faker.Entity.Vendor1.Id, vendor);

        // Assert
        vendorRepository.InnerList[0].Id.Should().Be(Faker.Entity.Vendor1.Id);
        vendorRepository.InnerList[0].FirstName.Should().Be(vendor.FirstName);

        result.Should().BeOfType<OkResult>();
    }


    [Fact]
    public async Task DeleteAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.Vendor2.Id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task DeleteAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.Vendor2.Id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task DeleteAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository, _systemClock);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.Vendor1.Id);

        // Assert
        result.Should().BeOfType<OkResult>();
    }
}