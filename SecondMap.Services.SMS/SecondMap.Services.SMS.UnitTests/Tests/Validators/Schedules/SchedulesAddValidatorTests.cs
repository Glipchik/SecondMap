using SecondMap.Services.SMS.API.Helpers.Validators.Schedules;
using SecondMap.Services.SMS.API.ViewModels.AddModels;

namespace SecondMap.Services.SMS.UnitTests.Tests.Validators.Schedules
{
	public class SchedulesAddValidatorTests
	{
		private readonly ScheduleAddValidator _validator;

		public SchedulesAddValidatorTests()
		{
			_validator = new ScheduleAddValidator();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenEveryFieldValid_ShouldReturnTrue(
			ScheduleAddViewModel validViewModel)
		{
			// Act
			var validationResult = await _validator.ValidateAsync(validViewModel);

			// Assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenInvalidStoreId_ShouldReturnFalse(
			ScheduleAddViewModel invalidViewModel)
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
			ScheduleAddViewModel invalidViewModel)
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
