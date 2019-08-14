using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class SubscribersTableConfiguration:IEntityTypeConfiguration<SubscribersTable>
    {
        public void Configure(EntityTypeBuilder<SubscribersTable> builder)
        {
            builder.HasKey(st => new { st.UserId, st.SubscriberId });

            builder.HasOne(st => st.User)
                .WithMany(u => u.Subscribers)
                .HasForeignKey(st => st.UserId);

            builder.HasOne(st => st.Subscriber)
                .WithMany()
                .HasForeignKey(st => st.SubscriberId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
