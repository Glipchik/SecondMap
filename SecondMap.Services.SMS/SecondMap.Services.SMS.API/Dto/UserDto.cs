using SecondMap.Services.SMS.DAL.Entities;

namespace SecondMap.Services.SMS.API.Dto
{
	public class UserDto
	{
		public int Id { get; set; }
		public string? Username { get; set; }

		public IEnumerable<ReviewDto>? Reviews { get; set; }

		public RoleEntity? Role { get; set; }
	}
}
