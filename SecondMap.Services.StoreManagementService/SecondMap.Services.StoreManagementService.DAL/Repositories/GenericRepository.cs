using Microsoft.EntityFrameworkCore;
using SecondMap.Services.StoreManagementService.DAL.Abstractions;
using SecondMap.Services.StoreManagementService.DAL.Context;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;

namespace SecondMap.Services.StoreManagementService.DAL.Repositories
{
	public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreManagementDbContext _dbContext;

		protected GenericRepository(StoreManagementDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);

			await _dbContext.SaveChangesAsync();
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<T?> UpdateAsync(T entity)
		{
			if (!_dbContext.Set<T>().Contains(entity))
				return null;

			_dbContext.Entry(entity).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();

			return entity;
		}
	}
}
