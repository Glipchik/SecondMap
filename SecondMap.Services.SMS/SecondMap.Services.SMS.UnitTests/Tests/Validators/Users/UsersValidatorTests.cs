namespace SecondMap.Services.SMS.UnitTests.Tests.Validators.Users
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

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenEveryFieldValid_ShouldReturnTrue(
			UserViewModel validViewModel)
		{
			// Arrange

			// Act
			var validationResult = await _validator.ValidateAsync(validViewModel);

			// Assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenEmptyName_ShouldReturnFalse(
			UserViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Username = string.Empty;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenTooLongName_ShouldReturnFalse(
			UserViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Username = string.Empty.PadRight(ValidationConstants.USER_NAME_MAX_LENGTH + 1, 'a');
			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenEmptyPassword_ShouldReturnFalse(
			UserViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Password = string.Empty;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenTooShortPassword_ShouldReturnFalse(
			UserViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Password = string.Empty.PadRight(ValidationConstants.USER_PASSWORD_MIN_LENGTH - 1, 'a');

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenTooLongPassword_ShouldReturnFalse(
			UserViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Password = string.Empty.PadRight(ValidationConstants.USER_PASSWORD_MAX_LENGTH + 1, 'a');

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}
	}
}
