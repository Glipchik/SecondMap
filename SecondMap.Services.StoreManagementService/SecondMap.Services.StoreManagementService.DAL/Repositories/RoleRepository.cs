using SecondMap.Services.StoreManagementService.DAL.Context;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.DAL.Repositories
{
	internal class RoleRepository : GenericRepository<Role>, IRoleRepository
	{
		public RoleRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}
	}
}
