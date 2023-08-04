namespace SecondMap.Services.SMS.DAL.Interfaces
{
	public interface ISoftDeletable
	{
		public bool IsDeleted { get; set; }
		public DateTimeOffset? DeletedAt { get; set; }

		public void Undo()
		{
			IsDeleted = false;
			DeletedAt = DateTimeOffset.MinValue;
		}

		public void SoftDelete()
		{
			IsDeleted = true;
			DeletedAt = null;
		}
	}
}
