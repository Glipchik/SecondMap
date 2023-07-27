namespace SecondMap.Services.StoreManagementService.UnitTests.TestClasses.Validators
{
	public class UsersValidatorTests
	{
		private readonly UsersValidator _validator;
		private readonly IFixture _fixture;

		public UsersValidatorTests()
		{
			_validator = new UsersValidator();
			_fixture = new Fixture();
		}

		[Fact]
		public async Task Validate_WhenEveryFieldValid_ShouldReturnTrue()
		{
			// Arrange
			var validViewModel = _fixture.Build<UserViewModel>()
				.OmitAutoProperties()
				.Do(u => u.Username = string.Empty.PadRight(ValidationConstants.USER_NAME_MAX_LENGTH - 1, 'a'))
				.Do(u => u.Password = string.Empty.PadRight(ValidationConstants.USER_PASSWORD_MIN_LENGTH + 1, 'a'))
				.Create();

			// Act
			var validationResult = await _validator.ValidateAsync(validViewModel);

			// Assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Fact]
		public async Task Validate_WhenEmptyName_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<UserViewModel>()
				.Create();
			invalidViewModel.Username = string.Empty;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenTooLongName_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<UserViewModel>()
				.Create();
			invalidViewModel.Username = string.Empty.PadRight(ValidationConstants.USER_NAME_MAX_LENGTH + 1, 'a');
			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenEmptyPassword_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<UserViewModel>().Create();
			invalidViewModel.Password = string.Empty;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenTooShortPassword_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<UserViewModel>()
				.Create();
			invalidViewModel.Password = string.Empty.PadRight(ValidationConstants.USER_PASSWORD_MIN_LENGTH - 1, 'a');

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenTooLongPassword_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<UserViewModel>()
				.Create();
			invalidViewModel.Password = string.Empty.PadRight(ValidationConstants.USER_PASSWORD_MAX_LENGTH + 1, 'a');

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}
	}
}
