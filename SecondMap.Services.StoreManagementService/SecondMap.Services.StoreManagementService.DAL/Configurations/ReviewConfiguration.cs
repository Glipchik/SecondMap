using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.DAL.Configurations
{
	public class ReviewConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.HasKey(r => r.Id);

			builder.Property(r => r.Description)
				.HasMaxLength(300);

			builder.Property(r => r.Rating).IsRequired();

			builder.HasOne(r => r.User)
				.WithMany()
				.HasForeignKey(r => r.UserId);

			builder.HasOne(r => r.Store)
				.WithMany()
				.HasForeignKey(r => r.StoreId);
		}
	}
}