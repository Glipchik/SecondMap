﻿using FluentValidation;
using SecondMap.Services.SMS.API.ViewModels;

namespace SecondMap.Services.SMS.API.Helpers.Validators
{
	public class UsersValidator : AbstractValidator<UserViewModel>
	{
		public UsersValidator()
		{
			RuleFor(u => u.Email).NotEmpty().EmailAddress();
			RuleFor(u => u.Role).IsInEnum();
		}
	}
}
