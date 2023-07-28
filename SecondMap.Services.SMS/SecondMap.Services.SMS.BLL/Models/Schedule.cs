using SecondMap.Services.SMS.DAL.Entities;
using SecondMap.Services.SMS.DAL.Enums;

namespace SecondMap.Services.SMS.BLL.Models
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
