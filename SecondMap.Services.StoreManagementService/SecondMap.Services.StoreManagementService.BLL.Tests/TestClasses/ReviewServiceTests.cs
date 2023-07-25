using SecondMap.Services.StoreManagementService.DAL.Entities;

namespace SecondMap.Services.StoreManagementService.BLL.Tests.TestClasses
{
	public class ReviewServiceTests
	{
		private readonly Mock<IReviewRepository> _reviewRepositoryMock;
		private readonly IMapper _mapper;
		private readonly IReviewService _reviewService;
		private readonly Fixture _fixture;
		public ReviewServiceTests()
		{
			_reviewRepositoryMock = new Mock<IReviewRepository>();
			_mapper = new Mapper(new MapperConfiguration(configuration =>
				configuration.AddProfile<ModelToEntityProfile>()
				));
			_reviewService = new ReviewService(_reviewRepositoryMock.Object, _mapper);
			_fixture = new Fixture();
		}

		[Fact]
		public async void GetAllAsync_ShouldReturnListOfReviews()
		{
			// Arrange
			var reviewModels = _fixture.Build<Review>().CreateMany().ToList();
			var reviewEntities = _mapper.Map<IEnumerable<ReviewEntity>>(reviewModels).ToList();

			_reviewRepositoryMock.Setup(r => r.GetAllAsync())
				.ReturnsAsync(reviewEntities);

			// Act 
			var reviewsFromTestMethod = (await _reviewService.GetAllAsync()).ToList();

			// Assert
			reviewsFromTestMethod.Should().AllBeOfType<Review>();
			reviewsFromTestMethod.Should().HaveCount(reviewModels.Count);
		}

		[Fact]
		public async void GetByIdAsync_WhenValidId_ShouldReturnReview()
		{
			// Arrange
			var reviewModel = _fixture.Build<Review>().Create();
			var reviewEntity = _mapper.Map<ReviewEntity>(reviewModel);

			_reviewRepositoryMock.Setup(r => r.GetByIdAsync(reviewModel.Id))
				.ReturnsAsync(reviewEntity);

			// Act 
			var foundReview = await _reviewService.GetByIdAsync(reviewModel.Id);

			// Assert
			foundReview.Should().NotBeNull();
			foundReview.Should().BeEquivalentTo(reviewModel);
		}

		[Fact]
		public async void GetByIdAsync_WhenInvalidId_ShouldThrowNotFoundException()
		{
			// Arrange
			_reviewRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act and Assert

			var exception = await Assert.ThrowsAsync<Exception>(
				async () => await _reviewService.GetByIdAsync(It.IsAny<int>()));

			exception.Message.Should().BeEquivalentTo(ErrorMessages.REVIEW_NOT_FOUND);
		}

		[Fact]
		public async void AddReviewAsync_WhenValidModel_ShouldReturnAddedModel()
		{
			// Arrange
			var reviewModel = _fixture.Build<Review>().Create();
			var reviewEntity = _mapper.Map<ReviewEntity>(reviewModel);

			_reviewRepositoryMock.Setup(r => r.AddAsync(It.IsAny<ReviewEntity>()))
				.ReturnsAsync(reviewEntity);

			// Act
			var addedReview = await _reviewService.AddReviewAsync(reviewModel);

			// Assert
			addedReview.Should().NotBeNull();
			addedReview.Should().BeOfType<Review>();
			addedReview.Should().BeEquivalentTo(reviewModel);
		}

		[Fact]
		public async Task UpdateReviewAsync_WhenValidReview_ShouldReturnUpdatedReview()
		{
			// Arrange
			var reviewModel = _fixture.Build<Review>().Create();
			var reviewEntity = _mapper.Map<ReviewEntity>(reviewModel);

			_reviewRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ReviewEntity>()))
				.ReturnsAsync(reviewEntity);

			// Act
			var updatedReview = await _reviewService.UpdateReviewAsync(reviewModel);

			// Assert
			updatedReview.Should().NotBeNull();
			updatedReview.Should().BeEquivalentTo(reviewModel);
		}

		[Fact]
		public async Task DeleteReviewAsync_WhenValidId_ShouldDeleteReview()
		{
			// Arrange
			var entities = _fixture.Build<ReviewEntity>().CreateMany();
			var entityIds = entities.Select(r => r.Id).ToList();
			var validId = entityIds[new Random().Next(entityIds.Count)];

			_reviewRepositoryMock.Setup(r => r.DeleteAsync(validId))
				.ReturnsAsync(true);

			// Act
			await _reviewService.DeleteReviewAsync(validId);

			// Assert
			_reviewRepositoryMock.Verify(r => r.DeleteAsync(validId), Times.Once);
		}
	}
}
