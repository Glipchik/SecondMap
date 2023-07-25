namespace SecondMap.Services.StoreManagementService.BLL.Tests.TestClasses
{
	public class ScheduleServiceTests
	{
		private readonly Mock<IScheduleRepository> _repositoryMock;
		private readonly IMapper _mapper;
		private readonly IScheduleService _service;
		private readonly Fixture _fixture;
		public ScheduleServiceTests()
		{
			_repositoryMock = new Mock<IScheduleRepository>();
			_mapper = new Mapper(new MapperConfiguration(configuration =>
				configuration.AddProfile<ModelToEntityProfile>()
				));
			_service = new ScheduleService(_repositoryMock.Object, _mapper);
			_fixture = new Fixture();
		}

		[Fact]
		public async void GetAllAsync_ShouldReturnListOfSchedules()
		{
			// Arrange
			var arrangedModels = _fixture.Build<Schedule>().CreateMany().ToList();
			var arrangedEntities = _mapper.Map<IEnumerable<ScheduleEntity>>(arrangedModels).ToList();

			_repositoryMock.Setup(r => r.GetAllAsync())
				.ReturnsAsync(arrangedEntities);

			// Act 
			var foundModels = (await _service.GetAllAsync()).ToList();

			// Assert
			foundModels.Should().AllBeOfType<Schedule>();
			foundModels.Should().HaveCount(arrangedModels.Count);
		}

		[Fact]
		public async void GetByIdAsync_WhenValidId_ShouldReturnSchedule()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Schedule>().Create();
			var arrangedEntity = _mapper.Map<ScheduleEntity>(arrangedModel);

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
			exception.Message.Should().BeEquivalentTo(ErrorMessages.SCHEDULE_NOT_FOUND);
		}

		[Fact]
		public async void AddScheduleAsync_WhenValidModel_ShouldReturnAddedModel()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Schedule>().Create();
			var arrangedEntity = _mapper.Map<ScheduleEntity>(arrangedModel);

			_repositoryMock.Setup(r => r.AddAsync(It.IsAny<ScheduleEntity>()))
				.ReturnsAsync(arrangedEntity);

			// Act
			var addedModel = await _service.AddScheduleAsync(arrangedModel);

			// Assert
			addedModel.Should().NotBeNull();
			addedModel.Should().BeOfType<Schedule>();
			addedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Fact]
		public async Task UpdateScheduleAsync_WhenValidSchedule_ShouldReturnUpdatedSchedule()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Schedule>().Create();
			var arrangedEntity = _mapper.Map<ScheduleEntity>(arrangedModel);

			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(arrangedEntity);

			_repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ScheduleEntity>()))
				.ReturnsAsync(arrangedEntity);

			// Act
			var updatedModel = await _service.UpdateScheduleAsync(arrangedModel);

			// Assert
			updatedModel.Should().NotBeNull();
			updatedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Fact]
		public async Task UpdateScheduleAsync_WhenInvalidSchedule_ShouldThrowNotFoundException()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Schedule>().Create();

			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act and Assert
			var exception = await Assert.ThrowsAsync<NotFoundException>(
				() => _service.UpdateScheduleAsync(arrangedModel));

			exception.Message.Should().BeEquivalentTo(ErrorMessages.SCHEDULE_NOT_FOUND);
		}

		[Fact]
		public async Task DeleteScheduleAsync_WhenValidId_ShouldDeleteSchedule()
		{
			// Arrange
			var arrangedEntity = _fixture.Build<ScheduleEntity>().Create();
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

			// Act and Assert
			var exception = await Assert.ThrowsAsync<NotFoundException>(
				() => _service.DeleteScheduleAsync(It.IsAny<int>()));
			exception.Message.Should().BeEquivalentTo(ErrorMessages.SCHEDULE_NOT_FOUND);
		}
	}
}
