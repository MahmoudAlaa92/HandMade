using HandMadeEcommece.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandMadeEcommece.Models.config
{
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
     
            builder.Property(x => x.Banner).HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(x=>x.Description).IsRequired().HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(x => x.FbLink).HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(x => x.InstaLink).HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(x => x.TwLink).HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(x => x.ShopName).HasColumnType("VARCHAR").HasMaxLength(255);

            
        }
    }
}
