using SecondMap.Services.StoreManagementService.DAL.Abstractions;

namespace SecondMap.Services.StoreManagementService.DAL.Entities
{
	public class UserEntity : BaseEntity
	{
		public string Username { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public string PasswordSalt { get; set; } = null!;
		public int RoleId { get; set; }

		public RoleEntity? Role { get; set; }
	}
}