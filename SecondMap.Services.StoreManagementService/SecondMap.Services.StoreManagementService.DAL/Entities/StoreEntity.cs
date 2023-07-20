using SecondMap.Services.StoreManagementService.DAL.Abstractions;

namespace SecondMap.Services.StoreManagementService.DAL.Entities
{
    public class StoreEntity : BaseEntity
	{
		public string Name { get; set; } = null!;
		public string Address { get; set; } = null!;
		public int? Rating { get; set; }
		public decimal Price { get; set; }
	}
}