using SecondMap.Services.SMS.DAL.Abstractions;

namespace SecondMap.Services.SMS.DAL.Entities
{
	public class StoreEntity : BaseEntity
	{
		public string Name { get; set; } = null!;
		public string Address { get; set; } = null!;
		public int? Rating { get; set; }
		public decimal Price { get; set; }

		public IEnumerable<ScheduleEntity>? Schedules { get; set; }
		public IEnumerable<ReviewEntity>? Reviews { get; set; }
	}
}