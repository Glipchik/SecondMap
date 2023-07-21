namespace SecondMap.Services.StoreManagementService.API.ViewModels
{
	public class ReviewViewModel
	{
		public int UserId { get; set; }
		public int StoreId { get; set; }
		public string? Description { get; set; }
		public int Rating { get; set; }
	}
}
