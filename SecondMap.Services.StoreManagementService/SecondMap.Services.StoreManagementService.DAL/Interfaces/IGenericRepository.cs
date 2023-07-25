using SecondMap.Services.StoreManagementService.DAL.Abstractions;

namespace SecondMap.Services.StoreManagementService.DAL.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		public Task<T> AddAsync(T entity);

		public Task<T?> UpdateAsync(T entity);

		public Task DeleteAsync(int id);

		public Task<T?> GetByIdAsync(int id);

		public Task<IEnumerable<T>> GetAllAsync();
	}
}
