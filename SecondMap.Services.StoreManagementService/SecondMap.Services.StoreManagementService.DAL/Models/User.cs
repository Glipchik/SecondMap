using SecondMap.Services.StoreManagementService.DAL.Abstractions;
using SecondMap.Services.StoreManagementService.DAL.Enums;

namespace SecondMap.Services.StoreManagementService.DAL.Models
{
	public class User : BaseEntity
	{
		public string Username { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public string PasswordSalt { get; set; } = null!;	
		public int RoleId { get; set; }

		public Role? Role { get; set; }
	}
}