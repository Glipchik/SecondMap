using SecondMap.Services.SMS.DAL.Abstractions;

namespace SecondMap.Services.SMS.DAL.Entities
{
	public class RoleEntity : BaseEntity
	{
		public string RoleName { get; set; } = null!;
	}
}