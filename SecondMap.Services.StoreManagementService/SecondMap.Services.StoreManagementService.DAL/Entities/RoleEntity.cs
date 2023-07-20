using SecondMap.Services.StoreManagementService.DAL.Abstractions;

namespace SecondMap.Services.StoreManagementService.DAL.Entities
{
    public class RoleEntity : BaseEntity
	{
		public string RoleName { get; set; } = null!;
	}
}