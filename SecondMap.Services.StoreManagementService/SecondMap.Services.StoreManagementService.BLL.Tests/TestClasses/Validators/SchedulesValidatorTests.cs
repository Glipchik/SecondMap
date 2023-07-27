namespace SecondMap.Services.StoreManagementService.UnitTests.TestClasses.Validators
{
	public class SchedulesValidatorTests
	{
		private readonly ScheduleValidator _validator;
		private readonly IFixture _fixture;

		public SchedulesValidatorTests()
		{
			_validator = new ScheduleValidator();
			_fixture = new Fixture();
		}

		[Fact]
		public async Task Validate_WhenEveryFieldValid_ShouldReturnTrue()
		{
			// Arrange
			var validViewModel = _fixture.Build<ScheduleViewModel>().Create();

			// Act
			var validationResult = await _validator.ValidateAsync(validViewModel);

			// Assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Fact]
		public async Task Validate_WhenInvalidStoreId_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<ScheduleViewModel>()
				.Create();
			invalidViewModel.StoreId = -1;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenInvalidDay_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<ScheduleViewModel>()
				.Create();
			invalidViewModel.Day = (DAL.Enums.DayOfWeekEu)(-1);

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}
	}
}
