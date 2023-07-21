using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace SecondMap.Services.StoreManagementService.API.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddValidators(this IServiceCollection services)
		{
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
				.AddFluentValidationAutoValidation();
		}
	}
}
