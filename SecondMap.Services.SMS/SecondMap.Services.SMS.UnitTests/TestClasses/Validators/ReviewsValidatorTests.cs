namespace SecondMap.Services.SMS.UnitTests.TestClasses.Validators
{
	public class ReviewsValidatorTests
	{
		private readonly ReviewValidator _validator;

		public ReviewsValidatorTests()
		{
			_validator = new ReviewValidator();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenEveryFieldValid_ShouldReturnTrue(
			ReviewViewModel validViewModel)
		{
			// Arrange

			// Act
			var validationResult = await _validator.ValidateAsync(validViewModel);

			// Assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenInvalidUserId_ShouldReturnFalse(
			ReviewViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.UserId = ValidationConstants.INVALID_ID;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenInvalidStoreId_ShouldReturnFalse(
			ReviewViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.StoreId = ValidationConstants.INVALID_ID;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenInvalidRating_ShouldReturnFalse(
			ReviewViewModel invalidViewModel)
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
			ReviewViewModel invalidViewModel)
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
