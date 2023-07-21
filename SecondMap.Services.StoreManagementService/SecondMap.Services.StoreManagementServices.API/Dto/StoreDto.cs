namespace SecondMap.Services.StoreManagementService.API.Dto
{
	public class StoreDto
	{
		public int Id { get; set; }
		public string? Name { get; set; } 
		public string? Address { get; set; }
		public int? Rating { get; set; }
		public decimal Price { get; set; }

		public List<ScheduleDto>? Schedules { get; set; }
	}
}
