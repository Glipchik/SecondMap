﻿using IdentityServer4.EntityFramework.Mappers;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecondMap.Services.Auth.Application.MessageConsumers;
using SecondMap.Services.Auth.Application.Services;
using SecondMap.Services.Auth.Domain.Entities;
using SecondMap.Services.Auth.Domain.Enums;
using SecondMap.Services.Auth.Infrastructure.Configurations;
using SecondMap.Services.Auth.Infrastructure.Context;
using System.Security.Claims;

namespace SecondMap.Services.Auth.Infrastructure.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddAuthDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AuthDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
			});
		}
		public static void InitializeDatabase(this IServiceCollection services)
		{
			var serviceProvider = services.BuildServiceProvider().CreateScope().ServiceProvider;

			var authDbContext = serviceProvider.GetRequiredService<AuthDbContext>();

			var userManager = serviceProvider.GetRequiredService<UserManager<AuthUser>>();
			var roleManager = serviceProvider.GetRequiredService<RoleManager<AuthRole>>();

			if (!authDbContext.IdentityResources.Any())
			{
				foreach (var identityResource in IdentityConfiguration.GetIdentityResources())
				{
					authDbContext.IdentityResources.Add(identityResource.ToEntity());
				}

				authDbContext.SaveChangesAsync().GetAwaiter().GetResult();
			}

			if (!authDbContext.ApiResources.Any())
			{
				foreach (var apiResource in IdentityConfiguration.GetApiResources())
				{
					authDbContext.ApiResources.Add(apiResource.ToEntity());
				}

				authDbContext.SaveChangesAsync().GetAwaiter().GetResult();
			}

			if (!authDbContext.ApiScopes.Any())
			{
				foreach (var apiScope in IdentityConfiguration.GetApiScopes())
				{
					authDbContext.ApiScopes.Add(apiScope.ToEntity());
				}

				authDbContext.SaveChangesAsync().GetAwaiter().GetResult();
			}

			if (!authDbContext.Clients.Any())
			{
				foreach (var client in IdentityConfiguration.GetClients())
				{
					authDbContext.Clients.Add(client.ToEntity());
				}

				authDbContext.SaveChangesAsync().GetAwaiter().GetResult();
			}

			if (!authDbContext.Roles.Any())
			{
				foreach (var role in IdentityConfiguration.GetRoles())
				{
					roleManager.CreateAsync(new AuthRole(role)).GetAwaiter().GetResult();
				}
			}

			if (!authDbContext.Users.Any())
			{
				var user = IdentityConfiguration.GetTestAuthUser();

				userManager.CreateAsync(user, "password")
					.GetAwaiter().GetResult();

				userManager.AddClaimsAsync(user, new List<Claim>
				{
					new Claim(ClaimTypes.Email, user.Email),
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(ClaimTypes.Role, Roles.Customer.ToString())
				}).GetAwaiter().GetResult();

				userManager.AddToRoleAsync(user, Roles.Customer.ToString()).GetAwaiter().GetResult();

				var admin = IdentityConfiguration.GetTestAdmin();

				userManager.CreateAsync(admin, "admin")
					.GetAwaiter().GetResult();

				userManager.AddClaimsAsync(admin, new List<Claim>
				{
					new Claim(ClaimTypes.Email, admin.Email),
					new Claim(ClaimTypes.Name, admin.UserName),
					new Claim(ClaimTypes.Role, Roles.Admin.ToString())
				}).GetAwaiter().GetResult();

				userManager.AddToRoleAsync(admin, Roles.Admin.ToString()).GetAwaiter().GetResult();
			}
		}

		public static void SetupIdentity(this IServiceCollection services)
		{
			services.AddIdentity<AuthUser, AuthRole>(options =>
			{
				options.User.RequireUniqueEmail = true;

				options.Password.RequireDigit = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 4;
			})
				.AddEntityFrameworkStores<AuthDbContext>()
				.AddDefaultTokenProviders();
		}

		public static void SetupIdentityServer(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");

			services.AddIdentityServer(options =>
			{
				options.Events.RaiseErrorEvents = true;
				options.Events.RaiseInformationEvents = true;
				options.Events.RaiseFailureEvents = true;
				options.Events.RaiseSuccessEvents = true;
			})
				.AddAspNetIdentity<AuthUser>()
				.AddProfileService<ProfileService>()
				.AddConfigurationStore<AuthDbContext>(options =>
				{
					options.ConfigureDbContext = builder =>
					{
						builder.UseSqlServer(connectionString);
					};
				})
				.AddOperationalStore<AuthDbContext>(options =>
				{
					options.ConfigureDbContext = builder =>
					{
						builder.UseSqlServer(connectionString);
						options.EnableTokenCleanup = true;
						options.TokenCleanupInterval = 3600;
					};
				})
				.AddDeveloperSigningCredential();
		}

		public static void SetupIdentityServerCookie(this IServiceCollection services)
		{
			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/auth/login";
				options.LogoutPath = "/auth/logout";
				options.Cookie.Name = "IdentityServer.Cookies";
			});
		}

		public static void AddRabbitMq(this IServiceCollection services)
		{
			services.AddMassTransit(mtConfig =>
			{
				mtConfig.SetKebabCaseEndpointNameFormatter();
				mtConfig.SetInMemorySagaRepositoryProvider();

				var assembly = typeof(UpdateUserConsumer).Assembly;

				mtConfig.AddConsumers(assembly);
				mtConfig.AddSagaStateMachines(assembly);
				mtConfig.AddSagas(assembly);
				mtConfig.AddActivities(assembly);

				mtConfig.UsingRabbitMq((context, rbConfig) =>
				{
					rbConfig.Host("localhost", h =>
					{
						h.Username("guest");
						h.Password("guest");
					});
					rbConfig.ConfigureEndpoints(context);
				});
			});
		}
	}
}
