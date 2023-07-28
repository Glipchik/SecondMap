namespace SecondMap.Services.SMS.UnitTests.TestClasses.Validators
{
	public class StoresValidatorTests
	{
		private readonly StoreValidator _validator;
		private readonly IFixture _fixture;

		public StoresValidatorTests()
		{
			_validator = new StoreValidator();
			_fixture = new Fixture();
		}

		[Fact]
		public async Task Validate_WhenEveryFieldValid_ShouldReturnTrue()
		{
			// Arrange
			var validViewModel = _fixture.Build<StoreViewModel>().Create();

			// Act
			var validationResult = await _validator.ValidateAsync(validViewModel);

			// Assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Fact]
		public async Task Validate_WhenEmptyName_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<StoreViewModel>()
				.Create();
			invalidViewModel.Name = string.Empty;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenTooLongName_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<StoreViewModel>()
				.Create();
			invalidViewModel.Name = string.Empty.PadRight(ValidationConstants.STORE_MAX_NAME_LENGTH + 1, 'a');
			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenEmptyAddress_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<StoreViewModel>().Create();
			invalidViewModel.Address = string.Empty;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenTooLongAddress_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<StoreViewModel>()
				.Create();
			invalidViewModel.Address = string.Empty.PadRight(ValidationConstants.STORE_MAX_ADDRESS_LENGTH + 1, 'a');

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenEmptyPrice_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<StoreViewModel>()
				.Create();
			invalidViewModel.Price = 0;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task Validate_WhenTooSmallPrice_ShouldReturnFalse()
		{
			// Arrange
			var invalidViewModel = _fixture.Build<StoreViewModel>()
				.Create();
			invalidViewModel.Price = ValidationConstants.STORE_MIN_PRICE - 1;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Arrange
			validationResult.IsValid.Should().BeFalse();
		}
	}
}
