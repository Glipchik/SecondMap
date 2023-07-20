namespace SecondMap.Services.StoreManagementService.API.Dto
{
	public class StoreDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Address { get; set; } = null!;
		public int? Rating { get; set; }
		public decimal Price { get; set; }
	}
}
