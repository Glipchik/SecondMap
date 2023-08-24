using SecondMap.Services.SMS.DAL.Enums;

namespace SecondMap.Services.SMS.BLL.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; } = null!;
		public string Email { get; set; } = null!;
		public UserRole Role { get; set; } = UserRole.Customer;

		public IEnumerable<Review>? Reviews { get; set; }
	}
}
