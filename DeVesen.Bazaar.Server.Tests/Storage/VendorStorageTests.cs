using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Tests.Fake.Repository;
using FluentAssertions;

namespace DeVesen.Bazaar.Server.Tests.Storage;

public class VendorStorageTests
{
    [Fact]
    public async Task ExistByIdAsync_EmptyRepository_ExpectFalse()
    {
        // Arrange
        var repository = new VendorRepositoryFake();
        var storage = new VendorStorage(repository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Vendor1.Id);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistByIdAsync_NotContainedEntity_ExpectFalse()
    {
        // Arrange
        var repository = new VendorRepositoryFake(Faker.Entity.Vendor2);
        var storage = new VendorStorage(repository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Vendor1.Id);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistByIdAsync_ContainedEntity_ExpectTrue()
    {
        // Arrange
        var repository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var storage = new VendorStorage(repository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Vendor1.Id);

        // Assert
        result.Should().BeTrue();
    }


    [Fact]
    public async Task GetAllAsync_EmptyRepository_ExpectEmpty()
    {
        // Arrange
        var repository = new VendorRepositoryFake();
        var storage = new VendorStorage(repository);

        // Act
        var result = await storage.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ContainedEntity_ExpectEntities()
    {
        // Arrange
        var expected = new[] { Faker.Entity.Vendor1, Faker.Entity.Vendor2, Faker.Entity.Vendor3 };
        var repository = new VendorRepositoryFake(Faker.Entity.Vendor1, Faker.Entity.Vendor2, Faker.Entity.Vendor3);
        var storage = new VendorStorage(repository);

        // Act
        var result = await storage.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }


    [Fact]
    public async Task CreateAsync_EmptyRepository_ExpectOneEntity()
    {
        // Arrange
        var repository = new VendorRepositoryFake();
        var storage = new VendorStorage(repository);

        // Act
        await storage.CreateAsync(Faker.Domain.Vendor2);

        // Assert
        repository.InnerList.Count.Should().Be(1);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntity_ExpectTwoEntity()
    {
        // Arrange
        var repository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var storage = new VendorStorage(repository);

        // Act
        await storage.CreateAsync(Faker.Domain.Vendor2);

        // Assert
        repository.InnerList.Count.Should().Be(2);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntityId_ExpectException()
    {
        // Arrange
        var repository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var storage = new VendorStorage(repository);
        var vendor = Faker.Domain.GetVendor(Faker.Entity.Vendor1.Id);

        // Act
        var act = () => storage.CreateAsync(vendor);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Entity.Vendor1.Id}' already exist!");
    }


    [Fact]
    public async Task UpdateAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var repository = new VendorRepositoryFake();
        var storage = new VendorStorage(repository);

        // Act
        var act = () => storage.UpdateAsync(Faker.Domain.Vendor2);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Vendor2.Id}' not found!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var repository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var storage = new VendorStorage(repository);

        // Act
        var act = () => storage.UpdateAsync(Faker.Domain.Vendor2);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Vendor2.Id}' not found!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var repository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var storage = new VendorStorage(repository);
        var vendor = Faker.Domain.GetVendor(id: Faker.Entity.Vendor1.Id,
            firstName: "Hello Vendor");

        // Act
        await storage.UpdateAsync(vendor);

        // Assert
        repository.InnerList[0].Id.Should().Be(vendor.Id);
        repository.InnerList[0].FirstName.Should().Be(vendor.FirstName);
    }


    [Fact]
    public async Task DeleteAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var repository = new VendorRepositoryFake();
        var storage = new VendorStorage(repository);

        // Act
        var act = () => storage.DeleteAsync(Faker.Domain.Vendor2.Id);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Vendor2.Id}' not found!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var repository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var storage = new VendorStorage(repository);

        // Act
        var act = () => storage.DeleteAsync(Faker.Domain.Vendor2.Id);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Vendor2.Id}' not found!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var repository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var storage = new VendorStorage(repository);

        // Act
        await storage.DeleteAsync(Faker.Domain.Vendor1.Id);

        // Assert
    }
}