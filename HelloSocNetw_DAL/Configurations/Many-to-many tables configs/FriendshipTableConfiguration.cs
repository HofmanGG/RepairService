using HelloSocNetw_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelloSocNetw_DAL.Configurations
{
    public class FriendshipTableConfiguration: IEntityTypeConfiguration<FriendshipTable>
    {
        public void Configure(EntityTypeBuilder<FriendshipTable> builder)
        {
            builder.HasKey(ft => new { ft.UserId, ft.FriendId });

            builder.HasOne(ft => ft.User)
                .WithMany()
                .HasForeignKey(ft => ft.UserId);

            builder.HasOne(ft => ft.Friend)
                .WithMany()
                .HasForeignKey(ft => ft.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
}
}