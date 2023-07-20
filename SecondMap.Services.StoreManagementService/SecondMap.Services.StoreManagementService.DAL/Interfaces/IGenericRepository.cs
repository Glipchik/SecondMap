using SecondMap.Services.StoreManagementService.DAL.Abstractions;

namespace SecondMap.Services.StoreManagementService.DAL.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		public Task AddAsync(T entity);

		public Task<T?> UpdateAsync(T entity);

		public Task DeleteAsync(T entity);

		public Task<T?> GetByIdAsync(int id);

		public Task<List<T>> GetAllAsync();
	}
}
