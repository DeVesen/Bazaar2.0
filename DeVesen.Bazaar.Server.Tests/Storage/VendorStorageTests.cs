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
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Vendor1.Id);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistByIdAsync_NotContainedEntity_ExpectFalse()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor2);
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Vendor1.Id);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistByIdAsync_ContainedEntity_ExpectTrue()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Vendor1.Id);

        // Assert
        result.Should().BeTrue();
    }


    [Fact]
    public async Task GetAllAsync_EmptyRepository_ExpectEmpty()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

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
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1, Faker.Entity.Vendor2, Faker.Entity.Vendor3);
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

        // Act
        var result = await storage.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }


    [Fact]
    public async Task CreateAsync_EmptyRepository_ExpectOneEntity()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

        // Act
        await storage.CreateAsync(Faker.Domain.Vendor2);

        // Assert
        vendorRepository.InnerList.Count.Should().Be(1);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntity_ExpectTwoEntity()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

        // Act
        await storage.CreateAsync(Faker.Domain.Vendor2);

        // Assert
        vendorRepository.InnerList.Count.Should().Be(2);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntityId_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);
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
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

        // Act
        var act = () => storage.UpdateAsync(Faker.Domain.Vendor2);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Vendor2.Id}' not found!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

        // Act
        var act = () => storage.UpdateAsync(Faker.Domain.Vendor2);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Vendor2.Id}' not found!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);
        var vendor = Faker.Domain.GetVendor(id: Faker.Entity.Vendor1.Id,
            firstName: "Hello Vendor");

        // Act
        await storage.UpdateAsync(vendor);

        // Assert
        vendorRepository.InnerList[0].Id.Should().Be(vendor.Id);
        vendorRepository.InnerList[0].FirstName.Should().Be(vendor.FirstName);
    }


    [Fact]
    public async Task DeleteAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

        // Act
        var act = () => storage.DeleteAsync(Faker.Domain.Vendor2.Id);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Vendor2.Id}' not found!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

        // Act
        var act = () => storage.DeleteAsync(Faker.Domain.Vendor2.Id);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Vendor2.Id}' not found!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake();
        var storage = new VendorStorage(vendorRepository, articleRepository);

        // Act
        await storage.DeleteAsync(Faker.Domain.Vendor1.Id);

        // Assert
    }
}