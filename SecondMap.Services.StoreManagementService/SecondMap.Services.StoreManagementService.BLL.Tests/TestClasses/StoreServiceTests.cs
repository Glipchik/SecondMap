namespace SecondMap.Services.StoreManagementService.BLL.Tests.TestClasses
{
	public class StoreServiceTests
	{
		private readonly Mock<IStoreRepository> _repositoryMock;
		private readonly IMapper _mapper;
		private readonly IStoreService _service;
		private readonly Fixture _fixture;
		public StoreServiceTests()
		{
			_repositoryMock = new Mock<IStoreRepository>();
			_mapper = new Mapper(new MapperConfiguration(configuration =>
				configuration.AddProfile<ModelToEntityProfile>()
				));
			_service = new StoreService(_repositoryMock.Object, _mapper);
			_fixture = new Fixture();
		}

		[Fact]
		public async void GetAllAsync_ShouldReturnListOfStores()
		{
			// Arrange
			var arrangedModels = _fixture.Build<Store>().CreateMany().ToList();
			var arrangedEntities = _mapper.Map<IEnumerable<StoreEntity>>(arrangedModels).ToList();

			_repositoryMock.Setup(r => r.GetAllAsync())
				.ReturnsAsync(arrangedEntities);

			// Act 
			var foundModels = (await _service.GetAllAsync()).ToList();

			// Assert
			foundModels.Should().AllBeOfType<Store>();
			foundModels.Should().HaveCount(arrangedModels.Count);
		}

		[Fact]
		public async void GetByIdAsync_WhenValidId_ShouldReturnStore()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Store>().Create();
			var arrangedEntity = _mapper.Map<StoreEntity>(arrangedModel);

			_repositoryMock.Setup(r => r.GetByIdAsync(arrangedModel.Id))
				.ReturnsAsync(arrangedEntity);

			// Act 
			var foundModel = await _service.GetByIdAsync(arrangedModel.Id);

			// Assert
			foundModel.Should().NotBeNull();
			foundModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Fact]
		public async void GetByIdAsync_WhenInvalidId_ShouldThrowNotFoundException()
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act
			var act = async () => await _service.GetByIdAsync(It.IsAny<int>());

			// Assert
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.STORE_NOT_FOUND);
		}

		[Fact]
		public async void AddStoreAsync_WhenValidModel_ShouldReturnAddedModel()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Store>().Create();
			var arrangedEntity = _mapper.Map<StoreEntity>(arrangedModel);

			_repositoryMock.Setup(r => r.AddAsync(It.IsAny<StoreEntity>()))
				.ReturnsAsync(arrangedEntity);

			// Act
			var addedModel = await _service.AddStoreAsync(arrangedModel);

			// Assert
			addedModel.Should().NotBeNull();
			addedModel.Should().BeOfType<Store>();
			addedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Fact]
		public async Task UpdateStoreAsync_WhenValidStore_ShouldReturnUpdatedStore()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Store>().Create();
			var arrangedEntity = _mapper.Map<StoreEntity>(arrangedModel);

			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(arrangedEntity);

			_repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<StoreEntity>()))
				.ReturnsAsync(arrangedEntity);

			// Act
			var updatedModel = await _service.UpdateStoreAsync(arrangedModel);

			// Assert
			updatedModel.Should().NotBeNull();
			updatedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Fact]
		public async Task UpdateStoreAsync_WhenInvalidStore_ShouldThrowNotFoundException()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Store>().Create();

			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act
			var act = async () => await _service.UpdateStoreAsync(arrangedModel);

			// Assert
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.STORE_NOT_FOUND);
		}

		[Fact]
		public async Task DeleteStoreAsync_WhenValidId_ShouldDeleteStore()
		{
			// Arrange
			var arrangedEntity = _fixture.Build<StoreEntity>().Create();
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
