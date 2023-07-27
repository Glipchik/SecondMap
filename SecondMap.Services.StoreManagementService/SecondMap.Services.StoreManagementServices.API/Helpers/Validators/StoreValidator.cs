using FluentValidation;
using SecondMap.Services.StoreManagementService.API.Constants;
using SecondMap.Services.StoreManagementService.API.ViewModels;

namespace SecondMap.Services.StoreManagementService.API.Helpers.Validators
{
	public class StoreValidator : AbstractValidator<StoreViewModel>
	{
		public StoreValidator()
		{
			RuleFor(s => s.Name).NotEmpty().MaximumLength(ValidationConstants.STORE_MAX_NAME_LENGTH);
			RuleFor(s => s.Address).NotEmpty().MaximumLength(ValidationConstants.STORE_MAX_ADDRESS_LENGTH);
			RuleFor(s => s.Price).NotEmpty();
			RuleFor(s => s.Price).GreaterThanOrEqualTo(ValidationConstants.STORE_MIN_PRICE);
		}
	}
}
