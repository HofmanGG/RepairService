using HelloSocNetw_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelloSocNetw_DAL.Configurations
{
    public class UserInfoConfiguration: IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.HasOne(u => u.AppIdentityUser)
                .WithOne(a => a.UserInfo)
                .HasForeignKey<UserInfo>(u => u.AppIdentityUserId);

            builder.Property(u => u.FirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(u => u.Gender)
                .IsRequired();
        }
    }
}