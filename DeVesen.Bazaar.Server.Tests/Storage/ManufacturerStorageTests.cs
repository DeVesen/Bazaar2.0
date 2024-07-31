using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Tests.Fake.Repository;
using FluentAssertions;

namespace DeVesen.Bazaar.Server.Tests.Storage;

public class ManufacturerStorageTests
{
    [Fact]
    public async Task ExistByIdAsync_EmptyRepository_ExpectFalse()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake();
        var storage = new ManufacturerStorage(repository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Manufacturer1.Id);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistByIdAsync_NotContainedEntity_ExpectFalse()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer2);
        var storage = new ManufacturerStorage(repository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Manufacturer1.Id);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistByIdAsync_ContainedEntity_ExpectTrue()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Manufacturer1.Id);

        // Assert
        result.Should().BeTrue();
    }


    [Fact]
    public async Task ExistByNameAsync_EmptyRepository_ExpectFalse()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake();
        var storage = new ManufacturerStorage(repository);

        // Act
        var result = await storage.ExistByNameAsync(Faker.Entity.Manufacturer1.Name);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistByNameAsync_NotContainedEntity_ExpectFalse()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer2);
        var storage = new ManufacturerStorage(repository);

        // Act
        var result = await storage.ExistByNameAsync(Faker.Entity.Manufacturer1.Name);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistByNameAsync_ContainedEntity_ExpectTrue()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);

        // Act
        var result = await storage.ExistByNameAsync(Faker.Entity.Manufacturer1.Name);

        // Assert
        result.Should().BeTrue();
    }


    [Fact]
    public async Task GetByNameAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake();
        var storage = new ManufacturerStorage(repository);

        // Act
        var act = () => storage.GetByNameAsync(Faker.Entity.Manufacturer1.Name);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Name '{Faker.Entity.Manufacturer1.Name}' not found!");
    }

    [Fact]
    public async Task GetByNameAsync_NotContainedEntity_ExpectException()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer2);
        var storage = new ManufacturerStorage(repository);

        // Act
        var act = () => storage.GetByNameAsync(Faker.Entity.Manufacturer1.Name);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Name '{Faker.Entity.Manufacturer1.Name}' not found!");
    }

    [Fact]
    public async Task GetByNameAsync_ContainedEntity_ExpectTrue()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);

        // Act
        var result = await storage.GetByNameAsync(Faker.Entity.Manufacturer1.Name);

        // Assert
        result.Should().Be(Faker.Domain.Manufacturer1);
    }


    [Fact]
    public async Task GetAllAsync_EmptyRepository_ExpectEmpty()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake();
        var storage = new ManufacturerStorage(repository);

        // Act
        var result = await storage.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ContainedEntity_ExpectEntities()
    {
        // Arrange
        var expected = new[] { Faker.Entity.Manufacturer1, Faker.Entity.Manufacturer2, Faker.Entity.Manufacturer3 };
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1, Faker.Entity.Manufacturer2, Faker.Entity.Manufacturer3);
        var storage = new ManufacturerStorage(repository);

        // Act
        var result = await storage.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }


    [Fact]
    public async Task CreateAsync_EmptyRepository_ExpectOneEntity()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake();
        var storage = new ManufacturerStorage(repository);

        // Act
        await storage.CreateAsync(Faker.Domain.Manufacturer2);

        // Assert
        repository.InnerList.Count.Should().Be(1);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntity_ExpectTwoEntity()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);

        // Act
        await storage.CreateAsync(Faker.Domain.Manufacturer2);

        // Assert
        repository.InnerList.Count.Should().Be(2);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntityId_ExpectException()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);

        // Act
        var act = () => storage.CreateAsync(Faker.Domain.GetManufacturer(Faker.Entity.Manufacturer1.Id, Faker.Entity.Manufacturer1.Name));

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Entity.Manufacturer1.Id}' already exist!");
    }

    [Fact]
    public async Task CreateAsync_ContainedEntityName_ExpectException()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);
        var entityId = Guid.Parse("59429FFE-EC6B-45BD-91CE-1075589B9668");

        // Act
        var act = () => storage.CreateAsync(Faker.Domain.GetManufacturer(entityId, Faker.Entity.Manufacturer1.Name));

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Name '{Faker.Entity.Manufacturer1.Name}' already exist!");
    }


    [Fact]
    public async Task UpdateAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake();
        var storage = new ManufacturerStorage(repository);

        // Act
        var act = () => storage.UpdateAsync(Faker.Domain.Manufacturer2);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Manufacturer2.Id}' not found!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);

        // Act
        var act = () => storage.UpdateAsync(Faker.Domain.Manufacturer2);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Manufacturer2.Id}' not found!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedOtherEntityWithName_ExpectException()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1, Faker.Entity.Manufacturer2);
        var storage = new ManufacturerStorage(repository);

        // Act
        var act = () => storage.UpdateAsync(Faker.Domain.GetManufacturer(Faker.Entity.Manufacturer2.Id, Faker.Entity.Manufacturer1.Name));

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Name '{Faker.Entity.Manufacturer1.Name}' already exist!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);
        var newName = "Hello FooBar";

        // Act
        await storage.UpdateAsync(Faker.Domain.GetManufacturer(Faker.Domain.Manufacturer1.Id, newName));

        // Assert
        repository.InnerList[0].Id.Should().Be(Faker.Domain.Manufacturer1.Id);
        repository.InnerList[0].Name.Should().Be(newName);
    }


    [Fact]
    public async Task DeleteAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake();
        var storage = new ManufacturerStorage(repository);

        // Act
        var act = () => storage.DeleteAsync(Faker.Domain.Manufacturer2.Id);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Manufacturer2.Id}' not found!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);

        // Act
        var act = () => storage.DeleteAsync(Faker.Domain.Manufacturer2.Id);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Manufacturer2.Id}' not found!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var repository = new ManufacturerRepositoryFake(Faker.Entity.Manufacturer1);
        var storage = new ManufacturerStorage(repository);

        // Act
        await storage.DeleteAsync(Faker.Domain.Manufacturer1.Id);

        // Assert
    }
}