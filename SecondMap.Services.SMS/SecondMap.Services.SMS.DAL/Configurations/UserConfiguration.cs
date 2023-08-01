using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondMap.Services.SMS.DAL.Entities;

namespace SecondMap.Services.SMS.DAL.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
	{
		public void Configure(EntityTypeBuilder<UserEntity> builder)
		{
			builder.HasKey(u => u.Id);

			builder.Property(u => u.Username).IsRequired();

			builder.Property(u => u.PasswordHash).IsRequired();

			builder.Property(u => u.PasswordSalt).IsRequired();

			builder.Property(u => u.RoleId).IsRequired();

			builder.HasOne(u => u.Role)
				.WithMany()
				.HasForeignKey(u => u.RoleId);

			builder.HasMany(u => u.Reviews)
				.WithOne(r => r.User)
				.HasForeignKey(r => r.UserId);
		}
	}
}