namespace SecondMap.Services.SMS.BLL.Models
{
	public class Review
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int StoreId { get; set; }
		public string? Description { get; set; }
		public int Rating { get; set; }

		public User? User { get; set; }
		public Store? Store { get; set; }
	}
}
