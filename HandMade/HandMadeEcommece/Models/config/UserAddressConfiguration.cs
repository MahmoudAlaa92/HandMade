using HandMadeEcommece.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandMadeEcommece.Models.config
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.State);
            builder.Property(x=>x.Address).IsRequired().HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(x => x.City).IsRequired().HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(x => x.Country).HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(x => x.Zip).HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(x => x.CreatedAt).HasColumnType("DATETIME");
            builder.Property(x => x.UpdatedAt).HasColumnType("DATETIME");

            builder.HasOne(e => e.User)
                .WithMany(e => e.UserAddresses)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);

            builder.HasOne(e=>e.Admin)
                .WithMany(e=>e.addresses)
                .HasForeignKey(e => e.AdminId)
                .IsRequired(false);

            builder.HasOne(e=>e.Vendor)
                .WithMany(e=>e.UserAddresses)
                .HasForeignKey(e=>e.VendorId)
                .IsRequired(false);
        }
    }
}
