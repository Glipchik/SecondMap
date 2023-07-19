using Microsoft.Extensions.DependencyInjection;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;
using SecondMap.Services.StoreManagementService.DAL.Repositories;

namespace SecondMap.Services.StoreManagementService.DAL.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDataAccessLayerServiceCollection(this IServiceCollection services)
		{
			services.AddScoped<IStoreRepository, StoreRepository>();

			services.AddScoped<IReviewRepository, ReviewRepository>();

			services.AddScoped<IScheduleRepository, ScheduleRepository>();

			services.AddScoped<IUserRepository, UserRepository>();

			return services;
		}
	}
}
