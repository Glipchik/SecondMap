using FluentValidation;
using SecondMap.Services.SMS.API.ViewModels;

namespace SecondMap.Services.SMS.API.Helpers.Validators
{
	public class ScheduleValidator : AbstractValidator<ScheduleViewModel>
	{
		public ScheduleValidator()
		{
			RuleFor(s => s.StoreId).Must(id => id > 0);
			RuleFor(s => s.Day).IsInEnum();
		}
	}
}
