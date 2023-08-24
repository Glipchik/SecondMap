using SecondMap.Services.SMS.DAL.Enums;

namespace SecondMap.Services.SMS.API.ViewModels
{
	public class UserViewModel
	{
		public string Email { get; set; } = null!;
		public UserRole Role { get; set; }
	}
}
