using SecondMap.Services.StoreManagementService.DAL.Abstractions;
using SecondMap.Services.StoreManagementService.DAL.Enums;

namespace SecondMap.Services.StoreManagementService.DAL.Models
{
	public class Schedule : BaseEntity
	{
		public int StoreId { get; set; }
		public DayOfWeekEu Day { get; set; }
		public TimeOnly OpeningTime { get; set; }
		public TimeOnly ClosingTime { get; set; }
		public bool IsClosed { get; set; }

		public Store? Store { get; set; }
	}
}