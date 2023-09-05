using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using SecondMap.Services.Auth.Domain.Entities;

namespace SecondMap.Services.Auth.Application.Services
{
	public class ProfileService : IProfileService
	{
		private readonly UserManager<AuthUser> _userManager;

		public ProfileService(UserManager<AuthUser> userManager)
		{
			_userManager = userManager;
		}

		public Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var user = _userManager.GetUserAsync(context.Subject).GetAwaiter().GetResult();

			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			var claims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult();

			context.IssuedClaims.AddRange(claims);

			return Task.CompletedTask;
		}

		public Task IsActiveAsync(IsActiveContext context)
		{
			context.IsActive = true;

			return Task.CompletedTask;
		}
	}
}
