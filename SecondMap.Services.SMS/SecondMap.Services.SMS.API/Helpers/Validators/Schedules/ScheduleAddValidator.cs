using FluentValidation;
using SecondMap.Services.SMS.API.Constants;
using SecondMap.Services.SMS.API.ViewModels.AddModels;

namespace SecondMap.Services.SMS.API.Helpers.Validators.Schedules
{
	public class ScheduleAddValidator : AbstractValidator<ScheduleAddViewModel>
	{
		public ScheduleAddValidator()
		{
			RuleFor(s => s.StoreId).Must(id => id > ValidationConstants.INVALID_ID);
			RuleFor(s => s.Day).IsInEnum();
		}
	}
}
