using Microsoft.AspNetCore.Identity;

namespace SecondMap.Services.Auth.Domain.Entities
{
	public class AuthRole : IdentityRole<int>
	{
		public AuthRole() : base()
		{ }

		public AuthRole(string roleName) : base(roleName)
		{ }
	}
}
