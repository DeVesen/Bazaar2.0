using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Tests.Fake.Repository;
using FluentAssertions;

namespace DeVesen.Bazaar.Server.Tests.Storage;

public class ArticleStorageTests
{
    [Fact]
    public async Task ExistByIdAsync_EmptyRepository_ExpectFalse()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var storage = new ArticleStorage(vendorRepository, articleRepository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Article1.Id);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistByIdAsync_NotContainedEntity_ExpectFalse()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article2);
        var storage = new ArticleStorage(vendorRepository, articleRepository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Article1.Id);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistByIdAsync_ContainedEntity_ExpectTrue()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var storage = new ArticleStorage(vendorRepository, articleRepository);

        // Act
        var result = await storage.ExistByIdAsync(Faker.Entity.Article1.Id);

        // Assert
        result.Should().BeTrue();
    }


    [Fact]
    public async Task GetAllAsync_EmptyRepository_ExpectEmpty()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var storage = new ArticleStorage(vendorRepository, articleRepository);

        // Act
        var result = await storage.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ContainedEntity_ExpectEntities()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1, Faker.Entity.Article2, Faker.Entity.Article3);
        var storage = new ArticleStorage(vendorRepository, articleRepository);
        var expected = new[] { Faker.Entity.Article1, Faker.Entity.Article2, Faker.Entity.Article3 };

        // Act
        var result = await storage.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }


    [Fact]
    public async Task CreateAsync_EmptyRepository_ExpectOneEntity()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor2);
        var articleRepository = new ArticleRepositoryFake();
        var storage = new ArticleStorage(vendorRepository, articleRepository);

        // Act
        await storage.CreateAsync(Faker.Domain.Article2);

        // Assert
        articleRepository.InnerList.Count.Should().Be(1);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntity_ExpectTwoEntity()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor2);
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var storage = new ArticleStorage(vendorRepository, articleRepository);

        // Act
        await storage.CreateAsync(Faker.Domain.Article2);

        // Assert
        articleRepository.InnerList.Count.Should().Be(2);
    }

    [Fact]
    public async Task CreateAsync_ContainedEntityId_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var storage = new ArticleStorage(vendorRepository, articleRepository);
        var article = Faker.Domain.GetArticle(Faker.Entity.Article1.Id);

        // Act
        var act = () => storage.CreateAsync(article);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Entity.Article1.Id}' already exist!");
    }


    [Fact]
    public async Task UpdateAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var storage = new ArticleStorage(vendorRepository, articleRepository);

        // Act
        var act = () => storage.UpdateAsync(Faker.Domain.Article2);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Article2.Id}' not found!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var storage = new ArticleStorage(vendorRepository, articleRepository);

        // Act
        var act = () => storage.UpdateAsync(Faker.Domain.Article2);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Article2.Id}' not found!");
    }

    [Fact]
    public async Task UpdateAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake(Faker.Entity.Vendor1);
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var storage = new ArticleStorage(vendorRepository, articleRepository);
        var article = Faker.Domain.GetArticle(vendorId: Faker.Entity.Vendor1.Id,
            id: Faker.Entity.Article1.Id,
            title: "Hello Article");

        // Act
        await storage.UpdateAsync(article);

        // Assert
        articleRepository.InnerList[0].Id.Should().Be(article.Id);
        articleRepository.InnerList[0].Title.Should().Be(article.Title);
    }


    [Fact]
    public async Task DeleteAsync_EmptyRepository_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake();
        var storage = new ArticleStorage(vendorRepository, articleRepository);

        // Act
        var act = () => storage.DeleteAsync(Faker.Domain.Article2.Id);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Article2.Id}' not found!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedOtherEntity_ExpectException()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var storage = new ArticleStorage(vendorRepository, articleRepository);

        // Act
        var act = () => storage.DeleteAsync(Faker.Domain.Article2.Id);

        // Assert
        (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.Article2.Id}' not found!");
    }

    [Fact]
    public async Task DeleteAsync_ContainedEntity_ExpectUpdate()
    {
        // Arrange
        var vendorRepository = new VendorRepositoryFake();
        var articleRepository = new ArticleRepositoryFake(Faker.Entity.Article1);
        var storage = new ArticleStorage(vendorRepository, articleRepository);

        // Act
        await storage.DeleteAsync(Faker.Domain.Article1.Id);

        // Assert
    }
}