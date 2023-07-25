namespace SecondMap.Services.StoreManagementService.BLL.Tests.TestClasses
{
	public class UserServiceTests
	{
		private readonly Mock<IUserRepository> _repositoryMock;
		private readonly IMapper _mapper;
		private readonly IUserService _service;
		private readonly Fixture _fixture;
		public UserServiceTests()
		{
			_repositoryMock = new Mock<IUserRepository>();
			_mapper = new Mapper(new MapperConfiguration(configuration =>
				configuration.AddProfile<ModelToEntityProfile>()
				));
			_service = new UserService(_repositoryMock.Object, _mapper);
			_fixture = new Fixture();
		}

		[Fact]
		public async void GetAllAsync_ShouldReturnListOfUsers()
		{
			// Arrange
			var arrangedModels = _fixture.Build<User>().CreateMany().ToList();
			var arrangedEntities = _mapper.Map<IEnumerable<UserEntity>>(arrangedModels).ToList();

			_repositoryMock.Setup(r => r.GetAllAsync())
				.ReturnsAsync(arrangedEntities);

			// Act 
			var foundModels = (await _service.GetAllAsync()).ToList();

			// Assert
			foundModels.Should().AllBeOfType<User>();
			foundModels.Should().HaveCount(arrangedModels.Count);
		}

		[Fact]
		public async void GetByIdAsync_WhenValidId_ShouldReturnUser()
		{
			// Arrange
			var arrangedModel = _fixture.Build<User>().Create();
			var arrangedEntity = _mapper.Map<UserEntity>(arrangedModel);

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

			// Act and Assert
			var exception = await Assert.ThrowsAsync<NotFoundException>(
				async () => await _service.GetByIdAsync(It.IsAny<int>()));
			exception.Message.Should().BeEquivalentTo(ErrorMessages.USER_NOT_FOUND);
		}

		[Fact]
		public async void AddUserAsync_WhenValidModel_ShouldReturnAddedModel()
		{
			// Arrange
			var arrangedModel = _fixture.Build<User>().Create();
			var arrangedEntity = _mapper.Map<UserEntity>(arrangedModel);

			_repositoryMock.Setup(r => r.AddAsync(It.IsAny<UserEntity>()))
				.ReturnsAsync(arrangedEntity);

			// Act
			var addedModel = await _service.AddUserAsync(arrangedModel);

			// Assert
			addedModel.Should().NotBeNull();
			addedModel.Should().BeOfType<User>();
			addedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Fact]
		public async Task UpdateUserAsync_WhenValidUser_ShouldReturnUpdatedUser()
		{
			// Arrange
			var arrangedModel = _fixture.Build<User>().Create();
			var arrangedEntity = _mapper.Map<UserEntity>(arrangedModel);

			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(arrangedEntity);

			_repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<UserEntity>()))
				.ReturnsAsync(arrangedEntity);

			// Act
			var updatedModel = await _service.UpdateUserAsync(arrangedModel);

			// Assert
			updatedModel.Should().NotBeNull();
			updatedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Fact]
		public async Task UpdateUserAsync_WhenInvalidUser_ShouldThrowNotFoundException()
		{
			// Arrange
			var arrangedModel = _fixture.Build<User>().Create();

			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act and Assert
			var exception = await Assert.ThrowsAsync<NotFoundException>(
				() => _service.UpdateUserAsync(arrangedModel));

			exception.Message.Should().BeEquivalentTo(ErrorMessages.USER_NOT_FOUND);
		}

		[Fact]
		public async Task DeleteUserAsync_WhenValidId_ShouldDeleteUser()
		{
			// Arrange
			var arrangedEntity = _fixture.Build<UserEntity>().Create();
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(arrangedEntity);

			_repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<UserEntity>()))
				.Returns(Task.CompletedTask);

			// Act
			await _service.DeleteUserAsync(It.IsAny<int>());

			// Assert
			_repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<UserEntity>()), Times.Once);
		}

		[Fact]
		public async Task DeleteUserAsync_WhenInvalidId_ShouldThrowNotFoundException()
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act and Assert
			var exception = await Assert.ThrowsAsync<NotFoundException>(
				() => _service.DeleteUserAsync(It.IsAny<int>()));
			exception.Message.Should().BeEquivalentTo(ErrorMessages.USER_NOT_FOUND);
		}
	}
}
