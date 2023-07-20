using Microsoft.EntityFrameworkCore;
using SecondMap.Services.StoreManagementService.DAL.Context;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;

namespace SecondMap.Services.StoreManagementService.DAL.Repositories
{
	internal class UserRepository : GenericRepository<UserEntity>, IUserRepository
	{
		public UserRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}

		public override async Task<UserEntity?> GetByIdAsync(int id)
		{
			return await _dbContext.Set<UserEntity>()
				.Where(u => u.Id == id)
				.Include(u => u.Role)
				.FirstOrDefaultAsync();
		}

		public override async Task<List<UserEntity>> GetAllAsync()
		{
			return await _dbContext.Set<UserEntity>()
				.Include(u => u.Role)
				.ToListAsync();
		}
	}
}
