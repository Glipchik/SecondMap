﻿using SecondMap.Services.SMS.API.Helpers.Validators.Reviews;
using SecondMap.Services.SMS.API.ViewModels.AddModels;

namespace SecondMap.Services.SMS.UnitTests.Tests.Validators.Reviews
{
	public class ReviewsAddValidatorTests
	{
		private readonly ReviewAddValidator _validator;

		public ReviewsAddValidatorTests()
		{
			_validator = new ReviewAddValidator();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenEveryFieldValid_ShouldReturnTrue(
			ReviewAddViewModel validViewModel)
		{
			// Act
			var validationResult = await _validator.ValidateAsync(validViewModel);

			// Assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenInvalidUserId_ShouldReturnFalse(
			ReviewAddViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.UserId = ValidationConstants.INVALID_ID;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Assert
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenInvalidStoreId_ShouldReturnFalse(
			ReviewAddViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.StoreId = ValidationConstants.INVALID_ID;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Assert
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenInvalidRating_ShouldReturnFalse(
			ReviewAddViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Rating = ValidationConstants.REVIEW_MIN_RATING - 1;

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Assert
			validationResult.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoMoqData]
		public async Task Validate_WhenInvalidDescription_ShouldReturnFalse(
			ReviewAddViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Description = string.Empty.PadRight(ValidationConstants.REVIEW_MAX_DESCRIPTION_LENGTH + 1, 'a');

			// Act
			var validationResult = await _validator.ValidateAsync(invalidViewModel);

			// Assert
			validationResult.IsValid.Should().BeFalse();
		}
	}
}
