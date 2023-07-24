using FluentValidation;
using SecondMap.Services.StoreManagementService.API.ViewModels;

namespace SecondMap.Services.StoreManagementService.API.Helpers.Validators
{
	public class UsersValidator : AbstractValidator<UserViewModel>
	{
		public UsersValidator()
		{
			RuleFor(u => u.Username).NotEmpty().MaximumLength(20);
			RuleFor(u => u.Password).NotEmpty().MinimumLength(8).MaximumLength(32);
		}
	}
}
