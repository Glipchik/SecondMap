using FluentValidation;
using SecondMap.Services.StoreManagementService.API.ViewModels;
using SecondMap.Services.StoreManagementService.BLL.Constants;

namespace SecondMap.Services.StoreManagementService.API.Helpers.Validators
{
	public class ScheduleValidator : AbstractValidator<ScheduleViewModel>
	{
		public ScheduleValidator()
		{
			RuleFor(s => s.StoreId).Must(id => id > 0);
			RuleFor(s => s.Day).IsInEnum();
			RuleFor(s => s.IsClosed).NotEmpty();

			RuleFor(s => s)
				.Custom((schedule, context) =>
				{
					if (schedule.IsClosed != null)
					{
						if ((bool)!schedule.IsClosed)
						{
							if (schedule.OpeningTime == null)
								context.AddFailure(ErrorMessages.VALIDATION_FAILED);

							if (schedule.ClosingTime == null)
								context.AddFailure(ErrorMessages.VALIDATION_FAILED);
						}
						else
						{
							if (schedule.OpeningTime != null)
								context.AddFailure(ErrorMessages.VALIDATION_FAILED);

							if (schedule.ClosingTime != null)
								context.AddFailure(ErrorMessages.VALIDATION_FAILED);
						}
					}
				});
		}
	}
}
