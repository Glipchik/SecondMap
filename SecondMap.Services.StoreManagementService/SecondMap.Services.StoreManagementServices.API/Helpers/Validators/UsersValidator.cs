using FluentValidation;
using SecondMap.Services.StoreManagementService.API.Constants;
using SecondMap.Services.StoreManagementService.API.ViewModels;

namespace SecondMap.Services.StoreManagementService.API.Helpers.Validators
{
	public class UsersValidator : AbstractValidator<UserViewModel>
	{
		public UsersValidator()
		{
			RuleFor(u => u.Username).NotEmpty().MaximumLength(ValidationConstants.USER_NAME_MAX_LENGTH);
			RuleFor(u => u.Password).NotEmpty()
				.MinimumLength(ValidationConstants.USER_PASSWORD_MIN_LENGTH)
				.MaximumLength(ValidationConstants.USER_PASSWORD_MAX_LENGTH);
		}
	}
}
