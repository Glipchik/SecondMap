using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.DAL.Configurations
{
	public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
	{
		public void Configure(EntityTypeBuilder<Schedule> builder)
		{
			builder.HasKey(s => s.Id);

			builder.Property(s => s.Day).IsRequired()
				.HasConversion<int>();

			builder.Property(s => s.OpeningTime).IsRequired();

			builder.Property(s => s.ClosingTime).IsRequired();

			builder.Property(s => s.IsClosed).IsRequired();

			builder.HasOne(s => s.Store)
				.WithMany()
				.HasForeignKey(s => s.StoreId);
		}
	}
}