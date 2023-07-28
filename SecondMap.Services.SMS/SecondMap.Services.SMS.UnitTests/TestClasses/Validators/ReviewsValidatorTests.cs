using SecondMap.Services.SMS.API.Constants;
using SecondMap.Services.SMS.API.Helpers.Validators;
using SecondMap.Services.SMS.API.ViewModels;

namespace SecondMap.Services.SMS.UnitTests.TestClasses.Validators
{
	public class ReviewsValidatorTests
	{
		private readonly ReviewValidator _validator;
		private readonly IFixture _fixture;

		public ReviewsValidatorTests()
		{
			_validator = new ReviewValidator();
			_fixture = new Fixture();
		}

		[Fact]
		public async Task Validate_WhenEveryFieldValid_ShouldReturnTrue()
		{
			// Arrange
			var validViewModel = _fixture.Build<ReviewViewModel>().Create();

			// Act
			var validationResult = await _validator.ValidateAsync(validViewModel);

			// Assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Fact]
		public async Task Validate_WhenInvalidUserId_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<ReviewViewModel>()
				.Create();
			invalidViewModel.UserId = -1;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenInvalidStoreId_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<ReviewViewModel>()
				.Create();
			invalidViewModel.StoreId = -1;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenInvalidRating_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<ReviewViewModel>().Create();
			invalidViewModel.Rating = ValidationConstants.REVIEW_MIN_RATING - 1;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenInvalidDescription_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<ReviewViewModel>()
				.Create();
			invalidViewModel.Description = string.Empty.PadRight(ValidationConstants.REVIEW_MAX_DESCRIPTION_LENGTH + 1, 'a');

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}
	}
}
