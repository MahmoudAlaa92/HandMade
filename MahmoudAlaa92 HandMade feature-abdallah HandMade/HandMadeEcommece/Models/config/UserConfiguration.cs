using HandMadeEcommece.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandMadeEcommece.Models.config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           
            //builder.Property(x => x.Image).HasColumnType("VARCHAR").HasMaxLength(255);//image

            builder.HasMany(e => e.Products)
                .WithMany(e => e.Users)
                .UsingEntity<WishList>();
        }
    }
}
