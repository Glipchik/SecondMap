namespace SecondMap.Services.StoreManagementService.API.Dto.AddingDto
{
	public class StoreAddingDto
	{
		public string Name { get; set; } = null!;
		public string Address { get; set; } = null!;
		public int? Rating { get; set; }
		public decimal Price { get; set; }
	}
}
