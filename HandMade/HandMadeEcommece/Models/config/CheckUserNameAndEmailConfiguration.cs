using HandMadeEcommece.Models.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandMadeEcommece.Models.config
{
    public class CheckUserNameAndEmailConfiguration : IEntityTypeConfiguration<CheckUserNameAndEmail>
    {
        public void Configure(EntityTypeBuilder<CheckUserNameAndEmail> builder)
        {
            builder.HasKey(e => new {e.UserName, e.Email});
            builder.Property(e => e.Email).IsRequired().HasColumnType("VARCHAR");
            builder.Property(e => e.UserName).IsRequired().HasColumnType("VARCHAR");
        }
    }
}
