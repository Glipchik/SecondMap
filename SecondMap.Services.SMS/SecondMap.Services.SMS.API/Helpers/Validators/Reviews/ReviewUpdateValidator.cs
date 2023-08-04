using FluentValidation;
using SecondMap.Services.SMS.API.Constants;
using SecondMap.Services.SMS.API.ViewModels.UpdateModels;

namespace SecondMap.Services.SMS.API.Helpers.Validators.Reviews
{
	public class ReviewUpdateValidator : AbstractValidator<ReviewUpdateViewModel>
	{
		public ReviewUpdateValidator()
		{
			RuleFor(r => r.Rating).InclusiveBetween(ValidationConstants.REVIEW_MIN_RATING,
				ValidationConstants.REVIEW_MAX_RATING);
			RuleFor(r => r.Description).MaximumLength(ValidationConstants.REVIEW_MAX_DESCRIPTION_LENGTH);
		}
	}
}
