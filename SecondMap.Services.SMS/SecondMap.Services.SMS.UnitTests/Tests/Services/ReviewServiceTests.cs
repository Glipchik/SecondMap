using System.Linq.Expressions;

namespace SecondMap.Services.SMS.UnitTests.Tests.Services
{
	public class ReviewServiceTests
	{
		private readonly Mock<IReviewRepository> _repositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly ReviewService _service;

		public ReviewServiceTests()
		{
			_repositoryMock = new Mock<IReviewRepository>();
			_mapperMock = new Mock<IMapper>();
			_service = new ReviewService(_repositoryMock.Object, _mapperMock.Object);
		}

		[Theory]
		[AutoMoqData]
		public async Task GetAllAsync_ShouldReturnListOfReviews(
			List<ReviewEntity> arrangedEntities,
			[Frozen] List<Review> arrangedModels)
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetAllAsync())
				.ReturnsAsync(arrangedEntities);

			_mapperMock.Setup(m => m.Map<IEnumerable<Review>>(It.IsAny<IEnumerable<ReviewEntity>>()))
				.Returns(arrangedModels);

			// Act 
			var foundModels = (await _service.GetAllAsync()).ToList();

			// Assert
			foundModels.Should().BeOfType<List<Review>>();
			foundModels.Should().AllBeOfType<Review>();
			foundModels.Should().BeEquivalentTo(arrangedModels);
		}

		[Theory]
		[AutoMoqData]
		public async Task GetAllByStoreIdAsync_WhenValidStoreIdAndStoreHasReviews_ShouldReturnReviews(
			List<ReviewEntity> arrangedEntities,
			[Frozen] List<Review> arrangedModels)
		{
			// Arrange
			arrangedEntities[0].User = new UserEntity()
			{
				Username = "test"
			};

			_repositoryMock.Setup(r => r.GetAllByPredicateAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()))
				.ReturnsAsync(arrangedEntities);

			_mapperMock.Setup(m => m.Map<IEnumerable<Review>>(It.IsAny<IEnumerable<ReviewEntity>>()))
				.Returns(arrangedModels);

			// Act
			var foundReviews = (await _service.GetAllByStoreIdAsync(It.IsAny<int>())).ToList();

			// Assert
			foundReviews.Should().BeOfType<List<Review>>();
			foundReviews.Should().AllBeOfType<Review>();
			foundReviews.Count.Should().Be(arrangedModels.Count);
			foundReviews[0].User.Should().NotBeNull();
		}

		[Fact]
		public async Task GetAllByStoreIdAsync_WhenInvalidStoreIdOrStoreHasNoReviews_ShouldThrowNotFoundException()
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetAllByPredicateAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()))
				.ReturnsAsync(new List<ReviewEntity>());

			_mapperMock.Setup(m => m.Map<IEnumerable<Review>>(It.IsAny<IEnumerable<ReviewEntity>>()))
				.Returns(new List<Review>());

			// Act
			var act = () =>_service.GetAllByStoreIdAsync(It.IsAny<int>());

			// Assert
			await act.Should().ThrowAsync<NotFoundException>()
				.WithMessage(ErrorMessages.REVIEWS_FOR_STORE_NOT_FOUND);
		}

		[Theory]
		[AutoMoqData]
		public async Task GetByIdAsync_WhenValidId_ShouldReturnReview(
			ReviewEntity reviewEntity,
			[Frozen] Review arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(reviewEntity);

			_mapperMock.Setup(m => m.Map<Review>(reviewEntity))
				.Returns(arrangedModel);

			// Act 
			var foundModel = await _service.GetByIdAsync(It.IsAny<int>());

			// Assert
			foundModel.Should().NotBeNull();
			foundModel.Should().BeOfType<Review>();
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
				.WithMessage(ErrorMessages.REVIEW_NOT_FOUND);
		}

		[Theory]
		[AutoMoqData]
		public async Task AddReviewAsync_WhenValidModel_ShouldReturnAddedModel(
			ReviewEntity arrangedEntity,
			[Frozen] Review arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.AddAsync(It.IsAny<ReviewEntity>()))
				.ReturnsAsync(arrangedEntity);

			_mapperMock.Setup(m => m.Map<Review>(It.IsAny<ReviewEntity>()))
				.Returns(arrangedModel);

			// Act
			var addedModel = await _service.AddReviewAsync(arrangedModel);

			// Assert
			addedModel.Should().NotBeNull();
			addedModel.Should().BeOfType<Review>();
			addedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Theory]
		[AutoMoqData]
		public async Task UpdateReviewAsync_WhenValidReview_ShouldReturnUpdatedReview(
			ReviewEntity arrangedEntity,
			[Frozen] Review arrangedModel)
		{
			// Arrange
			_repositoryMock.Setup(r => r.ExistsWithId(It.IsAny<int>()))
				.ReturnsAsync(true);

			_repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ReviewEntity>()))
				.ReturnsAsync(arrangedEntity);

			_mapperMock.Setup(m => m.Map<Review>(It.IsAny<ReviewEntity>()))
				.Returns(arrangedModel);

			// Act
			var updatedModel = await _service.UpdateReviewAsync(arrangedModel);

			// Assert
			updatedModel.Should().NotBeNull();
			updatedModel.Should().BeOfType<Review>();
			updatedModel.Should().BeEquivalentTo(arrangedModel);
		}

		[Theory]
		[AutoMoqData]
		public async Task UpdateReviewAsync_WhenInvalidReview_ShouldThrowNotFoundException(
			Review arrangedReview)
		{
			// Arrange
			_repositoryMock.Setup(r => r.ExistsWithId(It.IsAny<int>()))
				.ReturnsAsync(false);

			// Act
			var act = () => _service.UpdateReviewAsync(arrangedReview);

			// Assert
			await act.Should().ThrowAsync<NotFoundException>().WithMessage(ErrorMessages.REVIEW_NOT_FOUND);
		}

		[Theory]
		[AutoMoqData]
		public async Task DeleteReviewAsync_WhenValidId_ShouldDeleteReview(
			ReviewEntity arrangedEntity)
		{
			// Arrange
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
