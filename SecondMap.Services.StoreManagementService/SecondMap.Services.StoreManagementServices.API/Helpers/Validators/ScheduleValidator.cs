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

			When(s => s.IsClosed == false, () =>
			{
				RuleFor(s => s.OpeningTime)
					.NotEmpty().WithMessage("OpeningTime cannot be empty when store is open");

				RuleFor(s => s.ClosingTime)
					.NotEmpty().WithMessage("ClosingTime cannot be empty when store is open");
			});

			When(s => s.IsClosed == true, () =>
			{
				RuleFor(s => s.OpeningTime)
					.Empty().WithMessage("OpeningTime must be empty when store is closed");

				RuleFor(s => s.ClosingTime)
					.Empty().WithMessage("ClosingTime must be empty when store is closed");
			});
		}
	}
}
