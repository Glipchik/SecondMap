using SecondMap.Services.SMS.API.Helpers.Validators.Reviews;
using SecondMap.Services.SMS.API.ViewModels.UpdateModels;

namespace SecondMap.Services.SMS.UnitTests.Tests.Validators.Reviews
{
	public class ReviewsUpdateValidatorTests
	{
		private readonly ReviewUpdateValidator _validator;

		public ReviewsUpdateValidatorTests()
		{
			_validator = new ReviewUpdateValidator();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenEveryFieldValid_ShouldReturnTrue(
			ReviewUpdateViewModel validViewModel)
		{
			// Arrange

			// Act
			var validationResult = await _validator.ValidateAsync(validViewModel);

			// Assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenInvalidRating_ShouldReturnFalse(
			ReviewUpdateViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Rating = ValidationConstants.REVIEW_MIN_RATING;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenInvalidDescription_ShouldReturnFalse(
			ReviewUpdateViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Description = string.Empty.PadRight(ValidationConstants.REVIEW_MAX_DESCRIPTION_LENGTH + 1, 'a');

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}
	}
}
