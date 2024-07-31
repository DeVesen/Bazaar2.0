using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Tests.Fake.Repository;
using FluentAssertions;

namespace DeVesen.Bazaar.Server.Tests.Storage
{
    public class ArticleCategoryStorageTests
    {
        [Fact]
        public async Task ExistByIdAsync_EmptyRepository_ExpectFalse()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake();
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var result = await storage.ExistByIdAsync(Faker.Entity.ArticleCategory1.Id);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task ExistByIdAsync_NotContainedEntity_ExpectFalse()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory2);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var result = await storage.ExistByIdAsync(Faker.Entity.ArticleCategory1.Id);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task ExistByIdAsync_ContainedEntity_ExpectTrue()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var result = await storage.ExistByIdAsync(Faker.Entity.ArticleCategory1.Id);

            // Assert
            result.Should().BeTrue();
        }


        [Fact]
        public async Task ExistByNameAsync_EmptyRepository_ExpectFalse()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake();
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var result = await storage.ExistByNameAsync(Faker.Entity.ArticleCategory1.Name);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task ExistByNameAsync_NotContainedEntity_ExpectFalse()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory2);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var result = await storage.ExistByNameAsync(Faker.Entity.ArticleCategory1.Name);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task ExistByNameAsync_ContainedEntity_ExpectTrue()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var result = await storage.ExistByNameAsync(Faker.Entity.ArticleCategory1.Name);

            // Assert
            result.Should().BeTrue();
        }


        [Fact]
        public async Task GetByNameAsync_EmptyRepository_ExpectException()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake();
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var act = () => storage.GetByNameAsync(Faker.Entity.ArticleCategory1.Name);

            // Assert
            (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Name '{Faker.Entity.ArticleCategory1.Name}' not found!");
        }

        [Fact]
        public async Task GetByNameAsync_NotContainedEntity_ExpectException()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory2);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var act = () => storage.GetByNameAsync(Faker.Entity.ArticleCategory1.Name);

            // Assert
            (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Name '{Faker.Entity.ArticleCategory1.Name}' not found!");
        }

        [Fact]
        public async Task GetByNameAsync_ContainedEntity_ExpectTrue()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var result = await storage.GetByNameAsync(Faker.Entity.ArticleCategory1.Name);

            // Assert
            result.Should().Be(Faker.Domain.ArticleCategory1);
        }


        [Fact]
        public async Task GetAllAsync_EmptyRepository_ExpectEmpty()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake();
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var result = await storage.GetAllAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_ContainedEntity_ExpectEntities()
        {
            // Arrange
            var expected = new[] { Faker.Entity.ArticleCategory1, Faker.Entity.ArticleCategory2, Faker.Entity.ArticleCategory3 };
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1, Faker.Entity.ArticleCategory2, Faker.Entity.ArticleCategory3);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var result = await storage.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task CreateAsync_EmptyRepository_ExpectOneEntity()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake();
            var storage = new ArticleCategoryStorage(repository);

            // Act
            await storage.CreateAsync(Faker.Domain.ArticleCategory2);

            // Assert
            repository.InnerList.Count.Should().Be(1);
        }

        [Fact]
        public async Task CreateAsync_ContainedEntity_ExpectTwoEntity()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            await storage.CreateAsync(Faker.Domain.ArticleCategory2);

            // Assert
            repository.InnerList.Count.Should().Be(2);
        }

        [Fact]
        public async Task CreateAsync_ContainedEntityId_ExpectException()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var act = () => storage.CreateAsync(Faker.Domain.GetArticleCategory(Faker.Entity.ArticleCategory1.Id, Faker.Entity.ArticleCategory1.Name));

            // Assert
            (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Entity.ArticleCategory1.Id}' already exist!");
        }

        [Fact]
        public async Task CreateAsync_ContainedEntityName_ExpectException()
        {
            // Arrange
            const string entityId = "19";

            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var act = () => storage.CreateAsync(Faker.Domain.GetArticleCategory(entityId, Faker.Entity.ArticleCategory1.Name));

            // Assert
            (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Name '{Faker.Entity.ArticleCategory1.Name}' already exist!");
        }


        [Fact]
        public async Task UpdateAsync_EmptyRepository_ExpectException()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake();
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var act = () => storage.UpdateAsync(Faker.Domain.ArticleCategory2);

            // Assert
            (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.ArticleCategory2.Id}' not found!");
        }

        [Fact]
        public async Task UpdateAsync_ContainedOtherEntity_ExpectException()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var act = () => storage.UpdateAsync(Faker.Domain.ArticleCategory2);

            // Assert
            (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.ArticleCategory2.Id}' not found!");
        }

        [Fact]
        public async Task UpdateAsync_ContainedOtherEntityWithName_ExpectException()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1, Faker.Entity.ArticleCategory2);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var act = () => storage.UpdateAsync(Faker.Domain.GetArticleCategory(Faker.Entity.ArticleCategory2.Id, Faker.Entity.ArticleCategory1.Name));

            // Assert
            (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Name '{Faker.Entity.ArticleCategory1.Name}' already exist!");
        }

        [Fact]
        public async Task UpdateAsync_ContainedEntityWithName_ExpectException()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1, Faker.Entity.ArticleCategory2);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            await storage.UpdateAsync(Faker.Domain.ArticleCategory2);

            // Assert
        }

        [Fact]
        public async Task UpdateAsync_ContainedEntity_ExpectUpdate()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
            var storage = new ArticleCategoryStorage(repository);
            var newName = "Hello FooBar";

            // Act
            await storage.UpdateAsync(Faker.Domain.GetArticleCategory(Faker.Domain.ArticleCategory1.Id, newName));

            // Assert
            repository.InnerList[0].Id.Should().Be(Faker.Domain.ArticleCategory1.Id);
            repository.InnerList[0].Name.Should().Be(newName);
        }


        [Fact]
        public async Task DeleteAsync_EmptyRepository_ExpectException()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake();
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var act = () => storage.DeleteAsync(Faker.Domain.ArticleCategory2.Id);

            // Assert
            (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.ArticleCategory2.Id}' not found!");
        }

        [Fact]
        public async Task DeleteAsync_ContainedOtherEntity_ExpectException()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            var act = () => storage.DeleteAsync(Faker.Domain.ArticleCategory2.Id);

            // Assert
            (await act.Should().ThrowAsync<InvalidDataException>()).WithMessage($"Id '{Faker.Domain.ArticleCategory2.Id}' not found!");
        }

        [Fact]
        public async Task DeleteAsync_ContainedEntity_ExpectUpdate()
        {
            // Arrange
            var repository = new ArticleCategoryRepositoryFake(Faker.Entity.ArticleCategory1);
            var storage = new ArticleCategoryStorage(repository);

            // Act
            await storage.DeleteAsync(Faker.Domain.ArticleCategory1.Id);

            // Assert
        }
    }
}
