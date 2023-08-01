namespace SecondMap.Services.SMS.UnitTests.Tests.Validators
{
	public class SchedulesValidatorTests
	{
		private readonly ScheduleValidator _validator;

		public SchedulesValidatorTests()
		{
			_validator = new ScheduleValidator();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenEveryFieldValid_ShouldReturnTrue(
			ScheduleViewModel validViewModel)
		{
			// Arrange

			// Act
			var validationResult = await _validator.ValidateAsync(validViewModel);

			// Assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenInvalidStoreId_ShouldReturnFalse(
			ScheduleViewModel invalidViewModel)
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
		public async Task Validate_WhenInvalidDay_ShouldReturnFalse(
			ScheduleViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Day = (DayOfWeekEu)(-1);

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}
	}
}
