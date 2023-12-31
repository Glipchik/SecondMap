﻿using SecondMap.Services.SMS.DAL.Abstractions;
using SecondMap.Services.SMS.DAL.Enums;

namespace SecondMap.Services.SMS.DAL.Entities
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