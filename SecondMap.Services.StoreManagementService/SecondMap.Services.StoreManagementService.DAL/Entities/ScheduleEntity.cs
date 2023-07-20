using SecondMap.Services.StoreManagementService.DAL.Abstractions;
using SecondMap.Services.StoreManagementService.DAL.Enums;

namespace SecondMap.Services.StoreManagementService.DAL.Entities
{
	public class ScheduleEntity : BaseEntity
	{
		public int StoreId { get; set; }
		public DayOfWeekEu Day { get; set; }
		public TimeOnly OpeningTime { get; set; }
		public TimeOnly ClosingTime { get; set; }
		public bool IsClosed { get; set; }

		public StoreEntity? Store { get; set; }
	}
}