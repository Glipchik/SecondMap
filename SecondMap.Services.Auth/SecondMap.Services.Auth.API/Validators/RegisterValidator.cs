using FluentValidation;
using SecondMap.Services.Auth.API.RequestModels;
using SecondMap.Services.Auth.Application.Constants;

namespace SecondMap.Services.Auth.API.Validators
{
	public class RegisterValidator : AbstractValidator<RegisterRequestModel>
	{
		public RegisterValidator()
		{
			RuleFor(x => x.UserName).NotEmpty()
				.WithMessage(ErrorMessages.FIELD_REQUIRED);

			RuleFor(x => x.UserName).MinimumLength(4)
				.WithMessage(ErrorMessages.SHORT_USERNAME);

			RuleFor(x => x.Email).EmailAddress()
				.WithMessage(ErrorMessages.INVALID_EMAIL);

			RuleFor(x => x.Email).NotEmpty()
				.WithMessage(ErrorMessages.FIELD_REQUIRED);

			RuleFor(x => x.Password)
				.Equal(x => x.ConfirmPassword)
				.WithMessage(ErrorMessages.PASSWORD_MISMATCH);

			RuleFor(x => x.Password).NotEmpty()
				.WithMessage(ErrorMessages.FIELD_REQUIRED);

			RuleFor(x => x.Password).MinimumLength(8)
				.WithMessage(ErrorMessages.SHORT_PASSWORD);

			RuleFor(x => x.ConfirmPassword).NotEmpty()
				.WithMessage(ErrorMessages.FIELD_REQUIRED);
		}
	}
}
