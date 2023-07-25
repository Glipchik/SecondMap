using SecondMap.Services.StoreManagementService.DAL.Entities;

namespace SecondMap.Services.StoreManagementService.BLL.Tests.TestClasses
{
	public class ReviewServiceTests
	{
		private readonly Mock<IReviewRepository> _reviewRepositoryMock;
		private readonly IMapper _mapper;
		private readonly IReviewService _reviewService;

		public ReviewServiceTests()
		{
			_reviewRepositoryMock = new Mock<IReviewRepository>();
			_mapper = new Mapper(new MapperConfiguration(configuration =>
				configuration.AddProfile<ModelToEntityProfile>()
				));
			_reviewService = new ReviewService(_reviewRepositoryMock.Object, _mapper);
		}

		[Fact]
		public async void GetAllAsync_ShouldReturnListOfReviews()
		{
			// Arrange
			var fixture = new Fixture();

			var reviewModels = fixture.Build<Review>().CreateMany().ToList();
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
			var fixture = new Fixture();

			var reviewModel = fixture.Build<Review>().Create();
			var reviewEntity = _mapper.Map<ReviewEntity>(reviewModel);

			_reviewRepositoryMock.Setup(r => r.GetByIdAsync(reviewModel.Id))
				.ReturnsAsync(reviewEntity);

			// Act 
			var reviewFromTestMethod = await _reviewService.GetByIdAsync(reviewModel.Id);

			// Assert
			reviewFromTestMethod.Should().NotBeNull();
			reviewFromTestMethod.Should().BeEquivalentTo(reviewModel);
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
			var fixture = new Fixture();
			var reviewModel = fixture.Build<Review>().Create();
			var reviewEntity = _mapper.Map<ReviewEntity>(reviewModel);

			_reviewRepositoryMock.Setup(r => r.AddAsync(It.IsAny<ReviewEntity>()))
				.ReturnsAsync(reviewEntity);

			// Act
			var reviewFromTestMethod = await _reviewService.AddReviewAsync(reviewModel);

			// Assert
			reviewFromTestMethod.Should().NotBeNull();
			reviewFromTestMethod.Should().BeOfType<Review>();
			reviewFromTestMethod.Should().BeEquivalentTo(reviewModel);
		}

		[Fact]
		public async Task UpdateReviewAsync_WhenValidReview_ShouldReturnUpdatedReview()
		{
			// Arrange
			var fixture = new Fixture();
			var reviewModel = fixture.Build<Review>().Create();
			var reviewEntity = _mapper.Map<ReviewEntity>(reviewModel);

			_reviewRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ReviewEntity>()))
				.ReturnsAsync(reviewEntity);

			_mapperMock.Setup(m => m.Map<ReviewEntity>(It.IsAny<Review>()))
				.Returns(reviewEntity);

			_mapperMock.Setup(m => m.Map<Review>(It.IsAny<ReviewEntity>()))
				.Returns(reviewModel);

			// Act
			var updatedReview = await _reviewService.UpdateReviewAsync(reviewModel);

			// Assert
			updatedReview.Should().NotBeNull();
			updatedReview.Should().BeEquivalentTo(reviewModel);
		}

		[Fact]
		public async Task DeleteReviewAsync_WhenValidReview_ShouldDeleteReview()
		{
			// Arrange
			var reviewModel = new Review { Id = 1, Title = "Review Title", Rating = 4.5 };
			var reviewEntity = new ReviewEntity { Id = 1, Title = "Review Title", Rating = 4.5 };

			_repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<ReviewEntity>()));

			_mapperMock.Setup(m => m.Map<ReviewEntity>(It.IsAny<Review>()))
				.Returns(reviewEntity);

			// Act
			await _reviewService.DeleteReviewAsync(reviewModel);

			// Assert
			_repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ReviewEntity>()), Times.Once);
		}
	}
}
