using SecondMap.Services.SMS.DAL.Enums;

namespace SecondMap.Services.SMS.API.ViewModels.AddModels
{
	public class ScheduleAddViewModel 
	{
		public int StoreId { get; set; }
		public DayOfWeekEu Day { get; set; }
		public TimeOnly OpeningTime { get; set; }
		public TimeOnly ClosingTime { get; set; }
		public bool IsClosed { get; set; }
	}
}
