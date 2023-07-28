using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondMap.Services.SMS.DAL.Entities;

namespace SecondMap.Services.SMS.DAL.Configurations
{
	public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
	{
		public void Configure(EntityTypeBuilder<ReviewEntity> builder)
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