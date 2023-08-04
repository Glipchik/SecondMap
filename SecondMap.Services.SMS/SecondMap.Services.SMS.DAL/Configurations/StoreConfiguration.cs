using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondMap.Services.SMS.DAL.Entities;

namespace SecondMap.Services.SMS.DAL.Configurations
{
	public class StoreConfiguration : IEntityTypeConfiguration<StoreEntity>
	{
		public void Configure(EntityTypeBuilder<StoreEntity> builder)
		{
			builder.HasKey(s => s.Id);

			builder.Property(s => s.Name).IsRequired()
				.HasMaxLength(256);

			builder.Property(s => s.Address).IsRequired()
				.HasMaxLength(256);

			builder.Property(s => s.Rating);

			builder.Property(s => s.Price).IsRequired();

			builder.HasMany(s => s.Reviews)
				.WithOne(r => r.Store)
				.HasForeignKey(r => r.StoreId);

			builder.HasMany(s => s.Schedules)
				.WithOne(s => s.Store)
				.HasForeignKey(s => s.StoreId);

			builder.HasQueryFilter(s => s.IsDeleted == false);
				
		}
	}
}