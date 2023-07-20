using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Enums;

namespace SecondMap.Services.StoreManagementService.BLL.Models
{
	public class Schedule
	{
		public int Id { get; set; }
		public int StoreId { get; set; }
		public DayOfWeekEu Day { get; set; }
		public TimeOnly OpeningTime { get; set; }
		public TimeOnly ClosingTime { get; set; }
		public bool IsClosed { get; set; }

		public StoreEntity? Store { get; set; }
	}
}
