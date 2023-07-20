using SecondMap.Services.StoreManagementService.DAL.Entities;

namespace SecondMap.Services.StoreManagementService.API.Dto
{
	public class UserDto
	{
		public int Id { get; set; }
		public string Username { get; set; } = null!;
		public RoleEntity? Role { get; set; }
	}
}
