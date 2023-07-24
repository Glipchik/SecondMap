using AutoFixture;
using AutoMapper;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;
using SecondMap.Services.StoreManagementService.BLL.Services;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using System.Collections;
using FluentAssertions;
using SecondMap.Services.StoreManagementService.BLL.Constants;
using SecondMap.Services.StoreManagementService.BLL.MappingProfiles;

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
		public async void GetAll_ShouldReturnListOfReviews()
		{
			// Arrange
			var fixture = new Fixture();

			var reviewModels = fixture.Build<Review>().CreateMany().ToList();
			var reviewEntities = _mapper.Map<IEnumerable<ReviewEntity>>(reviewModels).ToList();

			_reviewRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(reviewEntities);

			// Act 
			var reviewsFromTestMethod = (await _reviewService.GetAllAsync()).ToList();

			// Assert
			reviewsFromTestMethod.Should().AllBeOfType<Review>();
			reviewsFromTestMethod.Should().HaveCount(reviewModels.Count);
		}

		[Fact]
		public async void GetById_WhenIdIsValid_ShouldReturnReview()
		{
			// Arrange
			var fixture = new Fixture();

			var reviewModel = fixture.Build<Review>().Create();
			var reviewEntity = _mapper.Map<ReviewEntity>(reviewModel);

			_reviewRepositoryMock
				.Setup(r => r.GetByIdAsync(reviewModel.Id))
				.ReturnsAsync(reviewEntity);

			// Act 
			var reviewFromTestMethod = await _reviewService.GetByIdAsync(reviewModel.Id);

			// Assert
			reviewFromTestMethod.Should().NotBeNull();
			reviewFromTestMethod.Should().BeEquivalentTo(reviewModel);
		}

		[Fact]
		public async void GetById_WhenIdIsInvalid_ShouldThrowNotFoundException()
		{
			// Arrange
			_reviewRepositoryMock
				.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(value: null);

			// Act and Assert

			var exception = await Assert.ThrowsAsync<Exception>(
				async () => await _reviewService.GetByIdAsync(It.IsAny<int>()));

			exception.Message.Should().BeEquivalentTo(ErrorMessages.REVIEW_NOT_FOUND);
		}

		[Fact]
		public async void Add_WhenModelIsValid_ShouldReturnAddedModel()
		{

		}

		[Fact]
		public async void Add_WhenModelIsInvalid_ShouldThrownValidationFailException()
		{

		}

		[Fact]
		public async void Update_WhenModelIsValid_ShouldReturnUpdatedModel()
		{

		}

		[Fact]
		public async void Update_WhenModelIsInvalid_ShouldThrowValidationFailException()
		{

		}

		[Fact]
		public async void Delete_WhenIdIsValid_ShouldDeleteAndReturnOk()
		{

		}

		[Fact]
		public async void Delete_WhenIdIsInvalid_ShouldThrowNotValidException()
		{

		}
	}
}
