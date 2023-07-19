using SecondMap.Services.StoreManagementService.DAL.Abstractions;

namespace SecondMap.Services.StoreManagementService.DAL.Models
{
    public class Role : BaseEntity
    {
	    public string RoleName { get; set; } = null!;
    }
}