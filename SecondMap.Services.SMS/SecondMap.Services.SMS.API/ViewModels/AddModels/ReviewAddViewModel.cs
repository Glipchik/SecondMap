namespace SecondMap.Services.SMS.API.ViewModels.AddModels
{
	public class ReviewAddViewModel
	{
		public int UserId { get; set; }
		public int StoreId { get; set; }
		public string? Description { get; set; }
		public int Rating { get; set; }
	}
}
