using SecondMap.Services.StoreManagementService.DAL.Abstractions;

namespace SecondMap.Services.StoreManagementService.DAL.Models
{
	public class Store : BaseEntity
	{
		public string Name { get; set; } = null!;
		public string Address { get; set; } = null!;
		public int? Rating { get; set; }
		public decimal Price { get; set; }
	}
}