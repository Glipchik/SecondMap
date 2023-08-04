namespace SecondMap.Services.SMS.API.Dto
{
	public class StoreDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Address { get; set; }
		public double? Rating { get; set; }
		public int ReviewCount { get; set; }
		public decimal Price { get; set; }

		public IEnumerable<ScheduleDto>? Schedules { get; set; }
		public IEnumerable<ReviewDto>? Reviews { get; set; }
	}
}
