using DeVesen.Bazaar.Server.Controllers;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Tests.Fake.Repository;
using DeVesen.Bazaar.Server.Validator;
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