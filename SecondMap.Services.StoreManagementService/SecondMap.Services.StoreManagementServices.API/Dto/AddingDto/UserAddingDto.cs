namespace SecondMap.Services.StoreManagementService.API.DTO.AddingDTO
{
	public class UserAddingDto
	{
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public int RoleId { get; set; } = 0;
	}
}
