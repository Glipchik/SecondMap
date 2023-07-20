using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondMap.Services.StoreManagementService.DAL.Entities;

namespace SecondMap.Services.StoreManagementService.DAL.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
	{
		public void Configure(EntityTypeBuilder<RoleEntity> builder)
		{
			builder.HasKey(r => r.Id);

			builder.Property(r => r.RoleName).IsRequired()
				.HasMaxLength(20);
		}
	}
}