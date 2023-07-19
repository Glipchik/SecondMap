namespace SecondMap.Services.StoreManagementService.API.Dto
{
	public class ReviewDto 
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int StoreId { get; set; }
		public string? Description { get; set; }
		public int Rating { get; set; }

		public UserDto? User { get; set; }
		public StoreDto? Store { get; set; }
	}
}
