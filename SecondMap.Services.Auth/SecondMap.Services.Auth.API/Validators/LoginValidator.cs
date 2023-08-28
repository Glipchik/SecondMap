using FluentValidation;
using SecondMap.Services.Auth.API.RequestModels;
using SecondMap.Services.Auth.Application.Constants;

namespace SecondMap.Services.Auth.API.Validators
{
	public class LoginValidator : AbstractValidator<LoginRequestModel>
	{
		public LoginValidator()
		{
			RuleFor(x => x.Email).EmailAddress()
				.WithMessage(ErrorMessages.INVALID_EMAIL);

			RuleFor(x => x.Email).NotEmpty()
				.WithMessage(ErrorMessages.FIELD_REQUIRED);

			RuleFor(x => x.Password).NotEmpty()
				.WithMessage(ErrorMessages.FIELD_REQUIRED);

			RuleFor(x => x.Password).MinimumLength(8)
				.WithMessage(ErrorMessages.SHORT_PASSWORD);
		}
	}
}
