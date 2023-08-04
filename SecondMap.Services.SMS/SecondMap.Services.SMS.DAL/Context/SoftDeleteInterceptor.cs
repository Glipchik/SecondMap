using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SecondMap.Services.SMS.DAL.Interfaces;

namespace SecondMap.Services.SMS.DAL.Context
{
	internal class SoftDeleteInterceptor : SaveChangesInterceptor
	{
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
			DbContextEventData eventData,
			InterceptionResult<int> result,
			CancellationToken cancellationToken = new CancellationToken())
		{
			if (eventData.Context == null)
			{
				return new ValueTask<InterceptionResult<int>>(result);
			}

			var entriesToDelete = new List<EntityEntry>();

			foreach (var entry in eventData.Context.ChangeTracker.Entries())
			{
				if (IsSoftDeletableEntry(entry))
				{
					SoftDeleteEntry(entry);
				}
				else
				{
					entriesToDelete.Add(entry);
				}
			}

			foreach (var entry in entriesToDelete)
			{
				entry.State = EntityState.Deleted;
			}

			return new ValueTask<InterceptionResult<int>>(result);
		}

		private static bool IsSoftDeletableEntry(EntityEntry entry)
		{
			return entry is { State: EntityState.Deleted, Entity: ISoftDeletable };
		}

		private static void SoftDeleteEntry(EntityEntry entry)
		{
			if (entry.Entity is ISoftDeletable softDeletableEntity)
			{
				entry.State = EntityState.Modified;
				softDeletableEntity.IsDeleted = true;
				softDeletableEntity.DeletedAt = DateTimeOffset.UtcNow;
			}

			foreach (var collectionEntry in entry.Collections)
			{
				if (!collectionEntry.IsLoaded)
				{
					collectionEntry.Load();
				}

				foreach (var relatedEntry in collectionEntry.CurrentValue)
				{
					SoftDeleteEntry((EntityEntry)relatedEntry);
				}
			}
		}
	}
}