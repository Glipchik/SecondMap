using SecondMap.Services.StoreManagementService.DAL.Abstractions;
using SecondMap.Services.StoreManagementService.DAL.Enums;

namespace SecondMap.Services.StoreManagementService.DAL.Models
{
	public class Schedule : BaseEntity
	{
		public int StoreId { get; set; }
		public DayOfWeekEu Day { get; set; }
		public TimeSpan OpeningTime { get; set; }
		public TimeSpan ClosingTime { get; set; }
		public bool IsClosed { get; set; }

		public Store? Store { get; set; }
	}
}