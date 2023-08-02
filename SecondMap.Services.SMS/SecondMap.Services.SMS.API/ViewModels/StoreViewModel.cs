namespace SecondMap.Services.SMS.API.ViewModels
{
	public class StoreViewModel : IViewModel
	{
		public string? Name { get; set; }
		public string? Address { get; set; }
		public decimal Price { get; set; }
	}
}
