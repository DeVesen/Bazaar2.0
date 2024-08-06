using DeVesen.Bazaar.Server.Controllers;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Tests.Fake.Repository;
using DeVesen.Bazaar.Server.Validator;
using DeVesen.Bazaar.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DeVesen.Bazaar.Server.Tests.Controller;

public class ManufacturerControllerTests
{

    [Fact]
    public async Task GetAllAsync_EmptyRepository_ExpectEmpty()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake();
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);

        // Act
        var result = await controller.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ContainedEntity_ExpectEntities()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1, Faker.Entity.Manufacturer2);
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);
        var expected = new[] { Faker.Dto.Manufacturer1, Faker.Dto.Manufacturer2 };

        // Act
        var result = await controller.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }


    [Fact]
    public async Task CreateAsync_EmptyRepository_ExpectOneEntity()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake();
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);

        // Act
        var result = await controller.CreateAsync(Faker.Dto.ManufacturerCreate01);

        // Assert
        repository.InnerList.Count.Should().Be(1);

        result.Should().BeOfType<OkResult>();
        ((OkResult)result).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntity_ExpectTwoEntity()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);

        // Act
        var result = await controller.CreateAsync(Faker.Dto.ManufacturerCreate02);

        // Assert
        repository.InnerList.Count.Should().Be(2);

        result.Should().BeOfType<OkResult>();
        ((OkResult)result).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntityName_ExpectBadRequest()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);

        // Act
        var result = await controller.CreateAsync(Faker.Dto.GetManufacturerCreate(Faker.Entity.Manufacturer1.Name));

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        ((BadRequestObjectResult)result).StatusCode.Should().Be(400);
        ((BadRequestObjectResult)result).Value.Should().Be("'Bezeichnung' ist bereits vergeben!");
    }


    [Fact]
    public async Task UpdateAsync_EmptyRepository_ExpectBadRequest()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake();
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);
        var dto = Faker.Dto.GetManufacturerUpdate("Update FooBar");

        // Act
        var result = await controller.UpdateAsync(Faker.Dto.Manufacturer2.Id, dto);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
        ((NotFoundObjectResult)result).Value.Should().Be($"Manufacturer '{Faker.Dto.Manufacturer2.Id}' nicht gefunden!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedOtherEntity_ExpectBadRequest()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);
        var dto = Faker.Dto.GetManufacturerUpdate("Update FooBar");

        // Act
        var result = await controller.UpdateAsync(Faker.Dto.Manufacturer2.Id, dto);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
        ((NotFoundObjectResult)result).Value.Should().Be($"Manufacturer '{Faker.Dto.Manufacturer2.Id}' nicht gefunden!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedOtherEntityWithName_ExpectBadRequest()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1, Faker.Entity.Manufacturer2);
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);
        var dto = Faker.Dto.GetManufacturerUpdate(Faker.Entity.Manufacturer1.Name);

        // Act
        var result = await controller.UpdateAsync(Faker.Entity.Manufacturer2.Id, dto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        ((BadRequestObjectResult)result).StatusCode.Should().Be(400);
        ((BadRequestObjectResult)result).Value.Should().Be($"'Bezeichnung' ist bereits vergeben!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);
        var dto = Faker.Dto.GetManufacturerUpdate("Hello FooBar");

        // Act
        var result = await controller.UpdateAsync(Faker.Domain.Manufacturer1.Id, dto);

        // Assert
        repository.InnerList[0].Id.Should().Be(Faker.Domain.Manufacturer1.Id);
        repository.InnerList[0].Name.Should().Be(dto.Name);

        result.Should().BeOfType<OkResult>();
    }


    [Fact]
    public async Task DeleteAsync_EmptyRepository_ExpectBadRequest()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake();
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.Manufacturer2.Id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
        ((NotFoundObjectResult)result).Value.Should().Be($"Manufacturer '{Faker.Dto.Manufacturer2.Id}' nicht gefunden!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedOtherEntity_ExpectBadRequest()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.Manufacturer2.Id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        ((NotFoundObjectResult)result).StatusCode.Should().Be(404);
        ((NotFoundObjectResult)result).Value.Should().Be($"Manufacturer '{Faker.Dto.Manufacturer2.Id}' nicht gefunden!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);
        var validator = new ManufacturerValidator(storage);
        var controller = new ManufacturerController(storage, validator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.Manufacturer1.Id);

        // Assert
        result.Should().BeOfType<OkResult>();
    }
}

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

public class VendorControllerTests
{
    [Fact]
    public async Task GetAllAsync_EmptyRepository_ExpectEmpty()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);

        // Act
        var result = await controller.GetAllAsync();

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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);
        var expected = new[] { Faker.Entity.Vendor1, Faker.Entity.Vendor2, Faker.Entity.Vendor3 };

        // Act
        var result = await controller.GetAllAsync();

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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
        var validator = new VendorValidator();
        var controller = new VendorController(vendorStorage, articleStorage, validator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.Vendor1.Id);

        // Assert
        result.Should().BeOfType<OkResult>();
    }
}

public class ArticleControllerTests
{
    [Fact]
    public async Task GetAllAsync_EmptyRepository_ExpectEmpty()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
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
        var articleStorage = new ArticleStorage(vendorRepository, articleRepository);
        var vendorStorage = new VendorStorage(vendorRepository, articleRepository);
        var articleValidator = new ArticleValidator(articleStorage, vendorStorage);
        var controller = new ArticleController(articleStorage, articleValidator);

        // Act
        var result = await controller.DeleteAsync(Faker.Domain.Article1.Id);

        // Assert
        result.Should().BeOfType<OkResult>();
    }
}