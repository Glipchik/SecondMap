using Microsoft.Extensions.DependencyInjection;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Services;

namespace SecondMap.Services.StoreManagementService.DAL.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBusinessLogicLayerServiceCollection(this IServiceCollection services)
		{
			services.AddScoped<IReviewService, ReviewService>();
			services.AddScoped<IScheduleService, ScheduleService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IStoreService, StoreService>();
			services.AddScoped<IUserService, UserService>();

			return services;
		}
	}
}
