using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.DAL.Configurations
{
	public class StoreConfiguration : IEntityTypeConfiguration<Store>
	{
		public void Configure(EntityTypeBuilder<Store> builder)
		{
			builder.HasKey(s => s.Id);

			builder.Property(s => s.Name).IsRequired()
				.HasMaxLength(256);

			builder.Property(s => s.Address).IsRequired()
				.HasMaxLength(256);

			builder.Property(s => s.Rating);

			builder.Property(s => s.Price).IsRequired();
		}
	}
}