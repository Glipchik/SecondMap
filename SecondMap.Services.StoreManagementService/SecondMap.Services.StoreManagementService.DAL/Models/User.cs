using SecondMap.Services.StoreManagementService.DAL.Abstractions;
using SecondMap.Services.StoreManagementService.DAL.Enums;

namespace SecondMap.Services.StoreManagementService.DAL.Models
{
	public class User : BaseEntity
	{
		public User(string username, string passwordHash, string passwordSalt)
		{
			Username = username;
			PasswordHash = passwordHash;
			PasswordSalt = passwordSalt;
		}

		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public string PasswordSalt { get; set; }
		public int RoleId { get; set; }

		public Role? Role { get; set; }
	}
}