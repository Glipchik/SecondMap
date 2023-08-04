using Microsoft.EntityFrameworkCore;
using SecondMap.Services.SMS.DAL.Abstractions;
using SecondMap.Services.SMS.DAL.Context;
using SecondMap.Services.SMS.DAL.Interfaces;
using System.Linq.Expressions;

namespace SecondMap.Services.SMS.DAL.Repositories
{
	public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		protected readonly StoreManagementDbContext _dbContext;

		protected GenericRepository(StoreManagementDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public virtual async Task<T> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);

			await _dbContext.SaveChangesAsync();

			return entity;
		}

		public virtual async Task DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);

			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public virtual async Task<T?> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public virtual async Task<T?> UpdateAsync(T entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();

			return entity;
		}

		public virtual async Task<bool> ExistsWithId(int id)
		{
			return await _dbContext.Set<T>().AnyAsync(e => e.Id == id);
		}

		public virtual async Task<IEnumerable<T>> GetAllByPredicateAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbContext.Set<T>().Where(predicate).ToListAsync();
		}
	}
}
