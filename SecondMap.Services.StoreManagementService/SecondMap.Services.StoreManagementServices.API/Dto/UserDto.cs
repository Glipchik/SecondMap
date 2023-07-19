using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.API.Dto
{
	public class UserDto
	{
		public int Id { get; set; }
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public int RoleId { get; set; }

		public Role? Role { get; set; }
	}
}
