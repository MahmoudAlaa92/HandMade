using HandMadeEcommece.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandMadeEcommece.Models.config
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x=>x.Price).IsRequired().HasPrecision(10,2);
            builder.Property(x=>x.SubTotal).IsRequired().HasPrecision(10,2);

            builder.HasOne(e => e.cart)
                .WithMany(e => e.items)
                .HasForeignKey(e => e.CartId)
                .IsRequired(false);

            builder.HasOne(e => e.productVariantItem)
               .WithMany(e => e.CartItems)
               .HasForeignKey(e => e.Product_Variant_Item_Id)
               .IsRequired(false);
        }
    }
}
