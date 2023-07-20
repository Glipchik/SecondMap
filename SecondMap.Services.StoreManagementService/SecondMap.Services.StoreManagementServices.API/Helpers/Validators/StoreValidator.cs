using FluentValidation;
using SecondMap.Services.StoreManagementService.API.ViewModels;

namespace SecondMap.Services.StoreManagementService.API.Helpers.Validators
{
	public class StoreValidator : AbstractValidator<StoreViewModel>
	{
		public StoreValidator()
		{
			RuleFor(s => s.Name).NotEmpty().MaximumLength(256);
			RuleFor(s => s.Address).NotEmpty().MaximumLength(256);
			RuleFor(s => s.Rating).Empty()
				.WithMessage("New store cant have rating yet");
			RuleFor(s => s.Price).NotEmpty();
		}
	}
}
