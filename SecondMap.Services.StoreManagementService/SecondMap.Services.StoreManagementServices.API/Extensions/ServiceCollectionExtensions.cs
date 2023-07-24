using FluentValidation;
using SecondMap.Services.StoreManagementService.API.Helpers.Validators;
using SecondMap.Services.StoreManagementService.API.ViewModels;

namespace SecondMap.Services.StoreManagementService.API.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddValidators(this IServiceCollection services)
		{
			services.AddScoped<IValidator<ReviewViewModel>, ReviewValidator>();
			services.AddScoped<IValidator<ScheduleViewModel>, ScheduleValidator>();
			services.AddScoped<IValidator<StoreViewModel>, StoreValidator>();
			services.AddScoped<IValidator<UserViewModel>, UsersValidator>();
		}
	}
}
