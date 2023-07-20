﻿using Microsoft.Extensions.DependencyInjection;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.MappingProfiles;
using SecondMap.Services.StoreManagementService.BLL.Services;
using SecondMap.Services.StoreManagementService.DAL.Extensions;

namespace SecondMap.Services.StoreManagementService.BLL.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddRepositories();

			services.AddScoped<IReviewService, ReviewService>();
			services.AddScoped<IScheduleService, ScheduleService>();
			services.AddScoped<IStoreService, StoreService>();
			services.AddScoped<IUserService, UserService>();

			services.AddAutoMapper(typeof(ModelToEntityProfile));
		}
	}
}
