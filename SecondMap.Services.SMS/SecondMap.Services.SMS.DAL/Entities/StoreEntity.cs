using SecondMap.Services.SMS.DAL.Abstractions;
using SecondMap.Services.SMS.DAL.Interfaces;

namespace SecondMap.Services.SMS.DAL.Entities
{
	public class StoreEntity : BaseEntity, ISoftDeletable
	{
		public string Name { get; set; } = null!;
		public string Address { get; set; } = null!;
		public decimal Price { get; set; }

		public IEnumerable<ScheduleEntity>? Schedules { get; set; }
		public IEnumerable<ReviewEntity>? Reviews { get; set; }
		public bool IsDeleted { get; set; }
		public DateTimeOffset? DeletedAt { get; set; }
	}
}
