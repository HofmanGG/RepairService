using HelloSocNetw_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelloSocNetw_DAL.Configurations
{
    public class DialogConfiguration : IEntityTypeConfiguration<Dialog>
    {
        public void Configure(EntityTypeBuilder<Dialog> builder)
        {
            builder.HasOne(st => st.FirstUser)
                .WithMany(u => u.Dialogs)
                .HasForeignKey(st => st.FirstUserId);

            builder.HasOne(st => st.SecondUser)
                .WithMany()
                .HasForeignKey(st => st.SecondUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
