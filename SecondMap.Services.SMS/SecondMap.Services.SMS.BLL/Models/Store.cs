namespace SecondMap.Services.SMS.BLL.Models
{
	public class Store
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Address { get; set; } = null!;

		public double? Rating
		{
			get
			{
				if (Reviews == null || !Reviews.Any())
					return null;

				ReviewCount = Reviews.Count();

				return Reviews.Average(r => r.Rating);
			}
		}

		public int ReviewCount { get; set; }

		public decimal Price { get; set; }

		public IEnumerable<Review>? Reviews { get; set; }
		public IEnumerable<Schedule>? Schedules { get; set; }
	}
}
