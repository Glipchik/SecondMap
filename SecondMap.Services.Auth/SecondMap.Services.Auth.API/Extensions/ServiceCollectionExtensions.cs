using FluentValidation;
using SecondMap.Services.Auth.API.RequestModels;
using SecondMap.Services.Auth.API.Validators;

namespace SecondMap.Services.Auth.API.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddValidations(this IServiceCollection services)
		{
			services.AddScoped<IValidator<LoginRequestModel>, LoginValidator>();
			services.AddScoped<IValidator<RegisterRequestModel>, RegisterValidator>();
		}
	}
}
