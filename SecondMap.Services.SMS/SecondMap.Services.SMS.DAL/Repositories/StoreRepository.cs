using Microsoft.EntityFrameworkCore;
using SecondMap.Services.SMS.DAL.Context;
using SecondMap.Services.SMS.DAL.Entities;
using SecondMap.Services.SMS.DAL.Interfaces;

namespace SecondMap.Services.SMS.DAL.Repositories
{
	public class StoreRepository : GenericRepository<StoreEntity>, IStoreRepository
	{
		public StoreRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<StoreEntity?> GetByIdWithDetailsAsync(int storeId)
		{
			return await _dbContext.Stores
				.Where(s => s.Id == storeId)
				// explicit select instead of Include coz Include doesn't work with Selects for Schedules and Reviews
				.Select(s => new StoreEntity
				{
					Id = s.Id,
					Name = s.Name,
					Address = s.Address,
					Rating = s.Rating,
					Price = s.Price,
					// excluding Store properties to avoid cyclic dependency
					Schedules = s.Schedules!.Select(sch => new ScheduleEntity
					{
						Id = sch.Id,
						StoreId = sch.StoreId,
						Day = sch.Day,
						OpeningTime = sch.OpeningTime,
						ClosingTime = sch.ClosingTime,
						IsClosed = sch.IsClosed
					}).ToList(),
					Reviews = s.Reviews!.Select(r => new ReviewEntity
					{
						Id = r.Id,
						UserId = r.UserId,
						StoreId = r.StoreId,
						Description = r.Description,
						Rating = r.Rating
					}).ToList()
				})
				.FirstOrDefaultAsync();
		}

	}
}
