﻿namespace SecondMap.Services.SMS.UnitTests.Tests.Validators.Stores
{
	public class StoresValidatorTests
	{
		private readonly StoreValidator _validator;

		public StoresValidatorTests()
		{
			_validator = new StoreValidator();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenEveryFieldValid_ShouldReturnTrue(
			StoreViewModel validViewModel)
		{
			// Act
			var validationResult = await _validator.ValidateAsync(validViewModel);

			// Assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenEmptyName_ShouldReturnFalse(
			StoreViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Name = string.Empty;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Assert
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenTooLongName_ShouldReturnFalse(
			StoreViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Name = string.Empty.PadRight(ValidationConstants.STORE_MAX_NAME_LENGTH + 1, 'a');
			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Assert
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenEmptyAddress_ShouldReturnFalse(
			StoreViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Address = string.Empty;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Assert
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenTooLongAddress_ShouldReturnFalse(
			StoreViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Address = string.Empty.PadRight(ValidationConstants.STORE_MAX_ADDRESS_LENGTH + 1, 'a');

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Assert
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenTooSmallPrice_ShouldReturnFalse(
			StoreViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Price = ValidationConstants.STORE_MIN_PRICE - 1;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Assert
			validationResult.IsValid.Should().BeFalse();
		}
	}
}
