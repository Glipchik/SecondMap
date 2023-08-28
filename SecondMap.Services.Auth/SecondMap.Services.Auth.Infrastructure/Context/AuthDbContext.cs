﻿using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecondMap.Services.Auth.Domain.Entities;

namespace SecondMap.Services.Auth.Infrastructure.Context
{
	public class AuthDbContext : IdentityDbContext<AuthUser, AuthRole, int>,
		IConfigurationDbContext, IPersistedGrantDbContext
	{
		public DbSet<Client>? Clients { get; set; }

		public DbSet<ApiResource>? ApiResources { get; set; }

		public DbSet<ApiScope>? ApiScopes { get; set; }

		public DbSet<IdentityResource>? IdentityResources { get; set; }

		public DbSet<ClientCorsOrigin>? ClientCorsOrigins { get; set; }

		public DbSet<PersistedGrant>? PersistedGrants { get; set; }

		public DbSet<DeviceFlowCodes>? DeviceFlowCodes { get; set; }

		public AuthDbContext(DbContextOptions<AuthDbContext> options)
			: base(options)
		{
			if (Database.IsRelational())
			{
				Database.Migrate();
			}
		}

		public Task<int> SaveChangesAsync()
		{
			return base.SaveChangesAsync();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<DeviceFlowCodes>().ToTable("DeviceCode").HasKey(x => x.UserCode);
			builder.Entity<PersistedGrant>().ToTable(nameof(PersistedGrant)).HasKey(x => x.Key);
		}
	}
}
