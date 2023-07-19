using Microsoft.EntityFrameworkCore;
using SecondMap.Services.StoreManagementService.DAL.Context;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.DAL.Repositories
{
	internal class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}

		public override async Task<User?> GetByIdAsync(int id)
		{
			return await _dbContext.Set<User>()
				.Where(u => u.Id == id)
				.Include(u => u.Role)
				.FirstOrDefaultAsync();
		}

		public override async Task<List<User>> GetAllAsync()
		{
			return await _dbContext.Set<User>()
				.Include(u => u.Role)
				.ToListAsync();
		}
	}
}
