namespace SecondMap.Services.StoreManagementService.UnitTests.TestClasses.Services
{
	public class ScheduleServiceTests
	{
		private readonly Mock<IScheduleRepository> _repositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly ScheduleService _service;

		public ScheduleServiceTests()
		{
			_repositoryMock = new Mock<IScheduleRepository>();
			_mapperMock = new Mock<IMapper>();
			_service = new ScheduleService(_repositoryMock.Object, _mapperMock.Object);
		}

		[Theory]
		[AutoMoqData]
		public async Task GetAllAsync_ShouldReturnListOfSchedules(
			List<ScheduleEntity> arrangedEntities,
			[Frozen] List<Schedule> arrangedModels)
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetAllAsync())
				.ReturnsAsync(arrangedEntities);

			_mapperMock.Setup(m => m.Map<IEnumerable<Schedule>>(It.IsAny<IEnumerable<ScheduleEntity>>()))
				.Returns(arrangedModels);

			// Act 
			var foundModels = (await _service.GetAllAsync()).ToList();

			// Assert
			foundModels.Should().BeOfType<List<Schedule>>();
			foundModels.Should().AllBeOfType<Schedule>();
			foundModels.Should().BeEquivalentTo(arrangedModels);
		}

		[Theory]
		[AutoMoqData]
		public async Task GetByIdAsync_WhenValidId_ShouldReturnSchedule(
			ScheduleEntity ScheduleEntity,
			[Frozen] Schedule arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(ScheduleEntity);

			_mapperMock.Setup(m => m.Map<Schedule>(ScheduleEntity))
				.Returns(arrangedModel);

			// Act 
			var foundModel = await _service.GetByIdAsync(It.IsAny<int>());

			// Assert
			foundModel.Should().NotBeNull();
			foundModel.Should().BeOfType<Schedule>();
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
				.WithMessage(ErrorMessages.SCHEDULE_NOT_FOUND);
		}

		[Theory]
		[AutoMoqData]
		public async Task AddScheduleAsync_WhenValidModel_ShouldReturnAddedModel(
			ScheduleEntity arrangedEntity,
			[Frozen] Schedule arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.AddAsync(It.IsAny<ScheduleEntity>()))
				.ReturnsAsync(arrangedEntity);

			_mapperMock.Setup(m => m.Map<Schedule>(It.IsAny<ScheduleEntity>()))
				.Returns(arrangedModel);

			// Act
			var addedModel = await _service.AddScheduleAsync(arrangedModel);

			// Assert
			addedModel.Should().NotBeNull();
			addedModel.Should().BeOfType<Schedule>();
			addedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Theory]
		[AutoMoqData]
		public async Task UpdateScheduleAsync_WhenValidSchedule_ShouldReturnUpdatedSchedule(
			ScheduleEntity arrangedEntity,
			[Frozen] Schedule arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.ExistsWithId(It.IsAny<int>()))
				.ReturnsAsync(true);

			_repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ScheduleEntity>()))
				.ReturnsAsync(arrangedEntity);

			_mapperMock.Setup(m => m.Map<Schedule>(It.IsAny<ScheduleEntity>()))
				.Returns(arrangedModel);

			// Act
			var updatedModel = await _service.UpdateScheduleAsync(arrangedModel);

			// Assert
			updatedModel.Should().NotBeNull();
			updatedModel.Should().BeOfType<Schedule>();
			updatedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Theory]
		[AutoMoqData]
		public async Task UpdateScheduleAsync_WhenInvalidSchedule_ShouldThrowNotFoundException(
			Schedule arrangedSchedule)
		{
			// Arrange
			_repositoryMock.Setup(r => r.ExistsWithId(It.IsAny<int>()))
				.ReturnsAsync(false);

			// Act
			var act = () => _service.UpdateScheduleAsync(arrangedSchedule);

			// Assert
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.SCHEDULE_NOT_FOUND);
		}

		[Theory]
		[AutoMoqData]
		public async Task DeleteScheduleAsync_WhenValidId_ShouldDeleteSchedule(
			ScheduleEntity arrangedEntity)
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(arrangedEntity);

			_repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<ScheduleEntity>()))
				.Returns(Task.CompletedTask);

			// Act
			await _service.DeleteScheduleAsync(It.IsAny<int>());

			// Assert
			_repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ScheduleEntity>()), Times.Once);
		}

		[Fact]
		public async Task DeleteScheduleAsync_WhenInvalidId_ShouldThrowNotFoundException()
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act
			var act = async () => await _service.DeleteScheduleAsync(It.IsAny<int>());

			// Assert
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.SCHEDULE_NOT_FOUND);
		}
	}
}
