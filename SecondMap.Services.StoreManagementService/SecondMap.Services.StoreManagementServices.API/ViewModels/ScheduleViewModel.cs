using SecondMap.Services.StoreManagementService.DAL.Enums;

namespace SecondMap.Services.StoreManagementService.API.ViewModels
{
	public class ScheduleViewModel
	{
		public int StoreId { get; set; }
		public DayOfWeekEu Day { get; set; }
		public TimeOnly OpeningTime { get; set; }
		public TimeOnly ClosingTime { get; set; }
		public bool IsClosed { get; set; }
	}
}
