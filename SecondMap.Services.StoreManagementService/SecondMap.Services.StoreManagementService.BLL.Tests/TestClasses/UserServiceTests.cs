namespace SecondMap.Services.StoreManagementService.BLL.Tests.TestClasses
{
	public class UserServiceTests
	{
		private readonly Mock<IUserRepository> _repositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly UserService _service;

		public UserServiceTests()
		{
			_repositoryMock = new Mock<IUserRepository>();
			_mapperMock = new Mock<IMapper>();
			_service = new UserService(_repositoryMock.Object, _mapperMock.Object);
		}

		[Theory]
		[AutoMoqData]
		public async Task GetAllAsync_ShouldReturnListOfUsers(
			List<UserEntity> arrangedEntities,
			[Frozen] List<User> arrangedModels)
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetAllAsync())
				.ReturnsAsync(arrangedEntities);

			_mapperMock.Setup(m => m.Map<IEnumerable<User>>(It.IsAny<IEnumerable<UserEntity>>()))
				.Returns(arrangedModels);

			// Act 
			var foundModels = (await _service.GetAllAsync()).ToList();

			// Assert
			foundModels.Should().BeOfType<List<User>>();
			foundModels.Should().AllBeOfType<User>();
			foundModels.Should().BeEquivalentTo(arrangedModels);
		}

		[Theory]
		[AutoMoqData]
		public async Task GetByIdAsync_WhenValidId_ShouldReturnUser(
			UserEntity UserEntity,
			[Frozen] User arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(UserEntity);

			_mapperMock.Setup(m => m.Map<User>(UserEntity))
				.Returns(arrangedModel);

			// Act 
			var foundModel = await _service.GetByIdAsync(It.IsAny<int>());

			// Assert
			foundModel.Should().NotBeNull();
			foundModel.Should().BeOfType<User>();
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
				.WithMessage(ErrorMessages.USER_NOT_FOUND);
		}

		[Theory]
		[AutoMoqData]
		public async Task AddUserAsync_WhenValidModel_ShouldReturnAddedModel(
			UserEntity arrangedEntity,
			[Frozen] User arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.AddAsync(It.IsAny<UserEntity>()))
				.ReturnsAsync(arrangedEntity);

			_mapperMock.Setup(m => m.Map<User>(It.IsAny<UserEntity>()))
				.Returns(arrangedModel);

			// Act
			var addedModel = await _service.AddUserAsync(arrangedModel);

			// Assert
			addedModel.Should().NotBeNull();
			addedModel.Should().BeOfType<User>();
			addedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Theory]
		[AutoMoqData]
		public async Task UpdateUserAsync_WhenValidUser_ShouldReturnUpdatedUser(
			UserEntity arrangedEntity,
			[Frozen] User arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.ExistsWithId(It.IsAny<int>()))
				.ReturnsAsync(true);

			_repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<UserEntity>()))
				.ReturnsAsync(arrangedEntity);

			_mapperMock.Setup(m => m.Map<User>(It.IsAny<UserEntity>()))
				.Returns(arrangedModel);

			// Act
			var updatedModel = await _service.UpdateUserAsync(arrangedModel);

			// Assert
			updatedModel.Should().NotBeNull();
			updatedModel.Should().BeOfType<User>();
			updatedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Theory]
		[AutoMoqData]
		public async Task UpdateUserAsync_WhenInvalidUser_ShouldThrowNotFoundException(
			User arrangedUser)
		{
			// Arrange
			_repositoryMock.Setup(r => r.ExistsWithId(It.IsAny<int>()))
				.ReturnsAsync(false);

			// Act
			var act = () => _service.UpdateUserAsync(arrangedUser);

			// Assert
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.USER_NOT_FOUND);
		}

		[Theory]
		[AutoMoqData]
		public async Task DeleteUserAsync_WhenValidId_ShouldDeleteUser(
			UserEntity arrangedEntity)
		{
			// Arrange
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

			// Act
			var act = async () => await _service.DeleteUserAsync(It.IsAny<int>());

			// Assert
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.USER_NOT_FOUND);
		}
	}
}
