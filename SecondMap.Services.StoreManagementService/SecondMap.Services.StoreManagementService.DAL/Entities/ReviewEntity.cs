using SecondMap.Services.StoreManagementService.DAL.Abstractions;

namespace SecondMap.Services.StoreManagementService.DAL.Entities
{
    public class ReviewEntity : BaseEntity
	{
		public int UserId { get; set; }
		public int StoreId { get; set; }
		public string? Description { get; set; }
		public int Rating { get; set; }

		public UserEntity? User { get; set; }
		public StoreEntity? Store { get; set; }
	}
}