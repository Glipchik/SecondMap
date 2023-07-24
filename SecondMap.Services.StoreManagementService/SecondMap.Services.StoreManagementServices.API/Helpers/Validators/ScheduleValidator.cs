using FluentValidation;
using SecondMap.Services.StoreManagementService.API.ViewModels;

namespace SecondMap.Services.StoreManagementService.API.Helpers.Validators
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
