using SecondMap.Services.SMS.DAL.Abstractions;
using System.Linq.Expressions;

namespace SecondMap.Services.SMS.DAL.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		public Task<T> AddAsync(T entity);

		public Task<T?> UpdateAsync(T entity);

		public Task DeleteAsync(T entity);

		public Task<T?> GetByIdAsync(int id);

		public Task<IEnumerable<T>> GetAllAsync();

		public Task<bool> ExistsWithId(int id);

		public Task<IEnumerable<T>> GetAllByPredicateAsync(Expression<Func<T, bool>> predicate);
	}
}
