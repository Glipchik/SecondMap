namespace SecondMap.Services.StoreManagementService.UnitTests.TestClasses.Services
{
	public class StoreServiceTests
	{
		private readonly Mock<IStoreRepository> _repositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly StoreService _service;

		public StoreServiceTests()
		{
			_repositoryMock = new Mock<IStoreRepository>();
			_mapperMock = new Mock<IMapper>();
			_service = new StoreService(_repositoryMock.Object, _mapperMock.Object);
		}

		[Theory]
		[AutoMoqData]
		public async Task GetAllAsync_ShouldReturnListOfStores(
			List<StoreEntity> arrangedEntities,
			[Frozen] List<Store> arrangedModels)
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetAllAsync())
				.ReturnsAsync(arrangedEntities);

			_mapperMock.Setup(m => m.Map<IEnumerable<Store>>(It.IsAny<IEnumerable<StoreEntity>>()))
				.Returns(arrangedModels);

			// Act 
			var foundModels = (await _service.GetAllAsync()).ToList();

			// Assert
			foundModels.Should().BeOfType<List<Store>>();
			foundModels.Should().AllBeOfType<Store>();
			foundModels.Should().BeEquivalentTo(arrangedModels);
		}

		[Theory]
		[AutoMoqData]
		public async Task GetByIdAsync_WhenValidId_ShouldReturnStore(
			StoreEntity StoreEntity,
			[Frozen] Store arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(StoreEntity);

			_mapperMock.Setup(m => m.Map<Store>(StoreEntity))
				.Returns(arrangedModel);

			// Act 
			var foundModel = await _service.GetByIdAsync(It.IsAny<int>());

			// Assert
			foundModel.Should().NotBeNull();
			foundModel.Should().BeOfType<Store>();
			foundModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Fact]
		public async Task GetByIdAsync_WhenInvalidId_ShouldThrowNotFoundException()
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act
			var act = () => _service.GetByIdAsync(It.IsAny<int>());

			await act.Should().ThrowAsync<NotFoundException>()
				.WithMessage(ErrorMessages.STORE_NOT_FOUND);
		}

		[Theory]
		[AutoMoqData]
		public async Task AddStoreAsync_WhenValidModel_ShouldReturnAddedModel(
			StoreEntity arrangedEntity,
			[Frozen] Store arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.AddAsync(It.IsAny<StoreEntity>()))
				.ReturnsAsync(arrangedEntity);

			_mapperMock.Setup(m => m.Map<Store>(It.IsAny<StoreEntity>()))
				.Returns(arrangedModel);

			// Act
			var addedModel = await _service.AddStoreAsync(arrangedModel);

			// Assert
			addedModel.Should().NotBeNull();
			addedModel.Should().BeOfType<Store>();
			addedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Theory]
		[AutoMoqData]
		public async Task UpdateStoreAsync_WhenValidStore_ShouldReturnUpdatedStore(
			StoreEntity arrangedEntity,
			[Frozen] Store arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.ExistsWithId(It.IsAny<int>()))
				.ReturnsAsync(true);

			_repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<StoreEntity>()))
				.ReturnsAsync(arrangedEntity);

			_mapperMock.Setup(m => m.Map<Store>(It.IsAny<StoreEntity>()))
				.Returns(arrangedModel);

			// Act
			var updatedModel = await _service.UpdateStoreAsync(arrangedModel);

			// Assert
			updatedModel.Should().NotBeNull();
			updatedModel.Should().BeOfType<Store>();
			updatedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Theory]
		[AutoMoqData]
		public async Task UpdateStoreAsync_WhenInvalidStore_ShouldThrowNotFoundException(
			Store arrangedStore)
		{
			// Arrange
			_repositoryMock.Setup(r => r.ExistsWithId(It.IsAny<int>()))
				.ReturnsAsync(false);

			// Act
			var act = () => _service.UpdateStoreAsync(arrangedStore);

			// Assert
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.STORE_NOT_FOUND);
		}

		[Theory]
		[AutoMoqData]
		public async Task DeleteStoreAsync_WhenValidId_ShouldDeleteStore(
			StoreEntity arrangedEntity)
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(arrangedEntity);

			_repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<StoreEntity>()))
				.Returns(Task.CompletedTask);

			// Act
			await _service.DeleteStoreAsync(It.IsAny<int>());

			// Assert
			_repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<StoreEntity>()), Times.Once);
		}

		[Fact]
		public async Task DeleteStoreAsync_WhenInvalidId_ShouldThrowNotFoundException()
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act
			var act = async () => await _service.DeleteStoreAsync(It.IsAny<int>());

			// Assert
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.STORE_NOT_FOUND);
		}
	}
}
