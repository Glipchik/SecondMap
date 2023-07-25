namespace SecondMap.Services.StoreManagementService.BLL.Tests.TestClasses
{
	public class ReviewServiceTests
	{
		private readonly Mock<IReviewRepository> _repositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly IReviewService _service;
		private readonly Fixture _fixture;
		public ReviewServiceTests()
		{
			_repositoryMock = new Mock<IReviewRepository>();
			_mapperMock = new Mock<IMapper>();
			_service = new ReviewService(_repositoryMock.Object, _mapperMock.Object);
			_fixture = new Fixture();
		}

		[Fact]
		public async void GetAllAsync_ShouldReturnListOfReviews()
		{
			// Arrange
			var arrangedModels = _fixture.Build<Review>().CreateMany().ToList();
			var arrangedEntities = _mapperMock.Object.Map<IEnumerable<ReviewEntity>>(arrangedModels).ToList();

			_repositoryMock.Setup(r => r.GetAllAsync())
				.ReturnsAsync(arrangedEntities);
			_mapper

			// Act 
			var foundModels = (await _service.GetAllAsync()).ToList();

			// Assert
			foundModels.Should().AllBeOfType<Review>();
			foundModels.Should().HaveCount(arrangedModels.Count);
		}

		[Fact]
		public async void GetByIdAsync_WhenValidId_ShouldReturnReview()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Review>().Create();
			var arrangedEntity = _mapperMock.Map<ReviewEntity>(arrangedModel);

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
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.REVIEW_NOT_FOUND);
		}

		[Fact]
		public async void AddReviewAsync_WhenValidModel_ShouldReturnAddedModel()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Review>().Create();
			var arrangedEntity = _mapperMock.Map<ReviewEntity>(arrangedModel);

			_repositoryMock.Setup(r => r.AddAsync(It.IsAny<ReviewEntity>()))
				.ReturnsAsync(arrangedEntity);

			// Act
			var addedModel = await _service.AddReviewAsync(arrangedModel);

			// Assert
			addedModel.Should().NotBeNull();
			addedModel.Should().BeOfType<Review>();
			addedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Fact]
		public async Task UpdateReviewAsync_WhenValidReview_ShouldReturnUpdatedReview()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Review>().Create();
			var arrangedEntity = _mapperMock.Map<ReviewEntity>(arrangedModel);

			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(arrangedEntity);

			_repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ReviewEntity>()))
				.ReturnsAsync(arrangedEntity);

			// Act
			var updatedModel = await _service.UpdateReviewAsync(arrangedModel);

			// Assert
			updatedModel.Should().NotBeNull();
			updatedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Fact]
		public async Task UpdateReviewAsync_WhenInvalidReview_ShouldThrowNotFoundException()
		{
			// Arrange
			var arrangedModel = _fixture.Build<Review>().Create();

			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act
			var act = () => _service.UpdateReviewAsync(arrangedModel);

			// Assert
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.REVIEW_NOT_FOUND);
		}

		[Fact]
		public async Task DeleteReviewAsync_WhenValidId_ShouldDeleteReview()
		{
			// Arrange
			var arrangedEntity = _fixture.Build<ReviewEntity>().Create();
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(arrangedEntity);

			_repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<ReviewEntity>()))
				.Returns(Task.CompletedTask);

			// Act
			await _service.DeleteReviewAsync(It.IsAny<int>());

			// Assert
			_repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ReviewEntity>()), Times.Once);
		}

		[Fact]
		public async Task DeleteReviewAsync_WhenInvalidId_ShouldThrowNotFoundException()
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act
			var act = async () => await _service.DeleteReviewAsync(It.IsAny<int>());

			// Assert
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.REVIEW_NOT_FOUND);
		}
	}
}
