using SecondMap.Services.SMS.DAL.Abstractions;
using SecondMap.Services.SMS.DAL.Interfaces;

namespace SecondMap.Services.SMS.DAL.Entities
{
	public class ReviewEntity : BaseEntity, ISoftDeletable
	{
		public int UserId { get; set; }
		public int StoreId { get; set; }
		public string? Description { get; set; }
		public int Rating { get; set; }

		public UserEntity? User { get; set; }
		public StoreEntity? Store { get; set; }
		public bool IsDeleted { get; set; }
		public DateTimeOffset? DeletedAt { get; set; }
	}
}