using SecondMap.Services.SMS.DAL.Abstractions;
using SecondMap.Services.SMS.DAL.Enums;

namespace SecondMap.Services.SMS.DAL.Entities
{
	public class UserEntity : BaseEntity
	{
		public string Username { get; set; } = null!;
		public string Email { get; set; } = null!;
		public UserRole? Role { get; set; } = UserRole.Customer;
		public IEnumerable<ReviewEntity>? Reviews { get; set; }
	}
}
