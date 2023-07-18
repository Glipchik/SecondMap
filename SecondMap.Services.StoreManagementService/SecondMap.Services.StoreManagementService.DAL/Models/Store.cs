using SecondMap.Services.StoreManagementService.DAL.Abstractions;

namespace SecondMap.Services.StoreManagementService.DAL.Models
{
    public class Store : BaseEntity
    {
        public Store(string name, string address)
        {
            Name = name;
            Address = address;
        }
                
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Rating { get; set; }
        public decimal Price { get; set; }
    }
}