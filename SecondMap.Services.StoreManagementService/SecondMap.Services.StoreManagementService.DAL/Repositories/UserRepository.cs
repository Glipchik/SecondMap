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
	}
}
