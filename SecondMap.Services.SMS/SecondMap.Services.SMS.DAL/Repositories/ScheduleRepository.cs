using SecondMap.Services.SMS.DAL.Context;
using SecondMap.Services.SMS.DAL.Entities;
using SecondMap.Services.SMS.DAL.Interfaces;

namespace SecondMap.Services.SMS.DAL.Repositories
{
	public class ScheduleRepository : GenericRepository<ScheduleEntity>, IScheduleRepository
	{
		public ScheduleRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}
	}
}
