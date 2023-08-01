using SecondMap.Services.SMS.DAL.Abstractions;

namespace SecondMap.Services.SMS.DAL.Entities
{
	public class UserEntity : BaseEntity
	{
		public string Username { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public string PasswordSalt { get; set; } = null!;
		public int RoleId { get; set; }

		public RoleEntity? Role { get; set; }
		public IEnumerable<ReviewEntity> Reviews { get; set; }

	}
}