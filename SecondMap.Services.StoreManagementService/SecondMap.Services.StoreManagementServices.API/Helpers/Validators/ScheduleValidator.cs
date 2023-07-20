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
			RuleFor(s => s.IsClosed).NotEmpty();

			RuleFor(s => s)
				.Custom((schedule, context) =>
				{
					if (schedule.IsClosed != null)
					{
						if ((bool)!schedule.IsClosed)
						{
							if (schedule.OpeningTime == null)
								context.AddFailure("OpeningTime cannot be empty when store is open");

							if (schedule.ClosingTime == null)
								context.AddFailure("ClosingTime cannot be empty when store is open");
						}
						else
						{
							if (schedule.OpeningTime != null)
								context.AddFailure("OpeningTime must be empty when store is closed");

							if (schedule.ClosingTime != null)
								context.AddFailure("ClosingTime must be empty when store is closed");
						}
					}
				});
		}
	}
}
