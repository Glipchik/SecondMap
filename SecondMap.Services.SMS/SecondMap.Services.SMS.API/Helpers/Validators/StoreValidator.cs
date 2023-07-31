using FluentValidation;
using SecondMap.Services.SMS.API.Constants;
using SecondMap.Services.SMS.API.ViewModels;

namespace SecondMap.Services.SMS.API.Helpers.Validators
{
	public class StoreValidator : AbstractValidator<StoreViewModel>
	{
		public StoreValidator()
		{
			RuleFor(s => s.Name).NotEmpty().MaximumLength(ValidationConstants.STORE_MAX_NAME_LENGTH);
			RuleFor(s => s.Address).NotEmpty().MaximumLength(ValidationConstants.STORE_MAX_ADDRESS_LENGTH);
			RuleFor(s => s.Price).Must(p => p > ValidationConstants.STORE_MIN_PRICE);
		}
	}
}
