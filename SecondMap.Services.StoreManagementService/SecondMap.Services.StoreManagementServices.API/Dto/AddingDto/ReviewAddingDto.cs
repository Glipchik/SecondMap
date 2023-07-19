using SecondMap.Services.StoreManagementService.API.Dto;

namespace SecondMap.Services.StoreManagementService.API.Dto.AddingDto
{
	public class ReviewAddingDto
	{
		public int UserId { get; set; }
		public int StoreId { get; set; }
		public string? Description { get; set; }
		public int Rating { get; set; }
	}
}
