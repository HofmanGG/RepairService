using System.Security.Policy;
using DAL.Entities;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class UserInfoConfiguration: IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.HasOne(u => u.AppIdentityUser)
                .WithOne(a => a.UserInfo)
                .HasForeignKey<AppIdentityUser>(u => u.UserInfoId);

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