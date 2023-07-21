using FluentValidation;
using SecondMap.Services.StoreManagementService.API.ViewModels;

namespace SecondMap.Services.StoreManagementService.API.Helpers.Validators
{
	public class ReviewValidator : AbstractValidator<ReviewViewModel>
	{
		public ReviewValidator()
		{
			RuleFor(r => r.UserId).Must(id => id > 0);
			RuleFor(r => r.StoreId).Must(id => id > 0);
			RuleFor(r => r.Rating).Must(rating => rating >= 0);
			RuleFor(r => r.Description).MaximumLength(300);
		}
	}
}
