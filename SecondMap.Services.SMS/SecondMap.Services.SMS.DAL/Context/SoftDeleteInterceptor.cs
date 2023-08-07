using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SecondMap.Services.SMS.DAL.Interfaces;

namespace SecondMap.Services.SMS.DAL.Context
{
	public class SoftDeleteInterceptor : SaveChangesInterceptor
	{
		private List<EntityEntry> _entriesToHardDelete = new();
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
			DbContextEventData eventData,
			InterceptionResult<int> result,
			CancellationToken cancellationToken = new())
		{
			if (eventData.Context == null)
			{
				return new ValueTask<InterceptionResult<int>>(result);
			}

			foreach (var entry in eventData.Context.ChangeTracker.Entries())
			{
				if (entry is not { State: EntityState.Deleted, Entity: ISoftDeletable })
				{
					continue;
				}

				SoftDeleteEntry(entry);
			}

			foreach (EntityEntry entityEntry in _entriesToHardDelete)
			{
				entityEntry.State = EntityState.Deleted;
			}

			return new ValueTask<InterceptionResult<int>>(result);
		}
		private void SoftDeleteEntry(EntityEntry entry)
		{
			// additional check cause this method is called later on related entries which might be hard deletable
			if (entry.Entity is ISoftDeletable softDeletableEntity)
			{
				entry.State = EntityState.Modified;
				softDeletableEntity.IsDeleted = true;
				softDeletableEntity.DeletedAt = DateTimeOffset.UtcNow;
			}

			// if entry is not soft deletable it means it is related entry which should be hard deleted
			else
			{
				_entriesToHardDelete.Add(entry);
			}

			foreach (var collectionEntry in entry.Collections)
			{
				if (!collectionEntry.IsLoaded)
				{
					collectionEntry.Load();
				}

				if (collectionEntry.CurrentValue == null)
				{
					continue;
				}

				foreach (var relatedEntity in collectionEntry.CurrentValue)
				{
					SoftDeleteEntry(collectionEntry.FindEntry(relatedEntity)!);
				}
			}
		}
	}
}