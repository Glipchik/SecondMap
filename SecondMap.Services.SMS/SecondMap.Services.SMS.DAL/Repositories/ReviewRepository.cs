using Microsoft.EntityFrameworkCore;
using SecondMap.Services.SMS.DAL.Context;
using SecondMap.Services.SMS.DAL.Entities;
using SecondMap.Services.SMS.DAL.Interfaces;
using System.Linq.Expressions;

namespace SecondMap.Services.SMS.DAL.Repositories
{
	public class ReviewRepository : GenericRepository<ReviewEntity>, IReviewRepository
	{
		public ReviewRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}

		public override async Task<IEnumerable<ReviewEntity>> GetAllByPredicateAsync(Expression<Func<ReviewEntity, bool>> predicate)
		{
			return await _dbContext.Reviews
				.Where(predicate)
				.Include(r => r.User)
				.ToListAsync();
		}
	}
}
