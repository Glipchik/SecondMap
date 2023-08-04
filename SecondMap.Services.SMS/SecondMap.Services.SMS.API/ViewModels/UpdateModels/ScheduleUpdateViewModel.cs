namespace SecondMap.Services.SMS.API.ViewModels.UpdateModels
{
	public class ScheduleUpdateViewModel 
	{
		public TimeOnly OpeningTime { get; set; }
		public TimeOnly ClosingTime { get; set; }
		public bool IsClosed { get; set; }
	}
}
