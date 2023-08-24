using SecondMap.Services.SMS.DAL.Enums;

namespace SecondMap.Services.SMS.API.Dto
{
	public class UserDto
	{
		public int Id { get; set; }
		public string? Username { get; set; }
		public string? Email { get; set; }
		public UserRole Role { get; set; }
		public IEnumerable<ReviewDto>? Reviews { get; set; }
	}
}
