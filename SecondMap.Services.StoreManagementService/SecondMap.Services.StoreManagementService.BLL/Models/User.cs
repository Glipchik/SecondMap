using SecondMap.Services.StoreManagementService.DAL.Entities;

namespace SecondMap.Services.StoreManagementService.BLL.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public string PasswordSalt { get; set; } = null!;
		public int RoleId { get; set; }

		public RoleEntity? Role { get; set; }
	}
}
