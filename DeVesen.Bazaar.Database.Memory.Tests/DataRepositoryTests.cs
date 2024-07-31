using FluentResults;

namespace DeVesen.Bazaar.Database.Memory.Tests
{
    public class DataRepositoryTests
    {
        private readonly DataContainer _dataContainer = new();

        [Fact]
        public async Task GetAllAsync_EmptyRepository_ExpectEmptyResult()
        {
            // Act
            IEnumerable<BaseDataEntity> entities = await _dataContainer.GetAllAsync<BaseDataEntity>();

            // Assert
            entities.Should().BeEmpty();
        }


        [Fact]
        public async Task Scenario_AddEntity_ReturnListOfAll()
        {
            // Arrange
            await _dataContainer.CreateAsync(new BaseDataEntity { Id = Guid.NewGuid() });

            // Act
            IEnumerable<BaseDataEntity> entities = await _dataContainer.GetAllAsync<BaseDataEntity>();

            // Assert
            entities.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Scenario_AddEntity_ExpectEntity()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            await _dataContainer.CreateAsync(new BaseDataEntity { Id = expectedId });

            // Act
            Result<BaseDataEntity> result = await _dataContainer.GetAsync<BaseDataEntity>(expectedId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.ValueOrDefault.Should().NotBeNull();
        }


        [Fact]
        public async Task Scenario_AddEntity_ExpectExist()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            await _dataContainer.CreateAsync(new BaseDataEntity { Id = expectedId });

            // Act
            Result<BaseDataEntity> result = await _dataContainer.ExistAsync(expectedId);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

    }
}
