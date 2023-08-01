using FluentValidation;
using SecondMap.Services.SMS.API.Constants;
using SecondMap.Services.SMS.API.ViewModels.AddModels;

namespace SecondMap.Services.SMS.API.Helpers.Validators.Reviews
{
    public class ReviewAddValidator : AbstractValidator<ReviewAddViewModel>
    {
        public ReviewAddValidator()
        {
            RuleFor(r => r.UserId).Must(id => id > ValidationConstants.INVALID_ID);
            RuleFor(r => r.StoreId).Must(id => id > ValidationConstants.INVALID_ID);
            RuleFor(r => r.Rating).Must(rating => rating > ValidationConstants.REVIEW_MIN_RATING);
            RuleFor(r => r.Description).MaximumLength(ValidationConstants.REVIEW_MAX_DESCRIPTION_LENGTH);
        }
    }
}
