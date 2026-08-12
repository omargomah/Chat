using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Chat.Entities;
using MVC.Chat.ValueObject;

namespace MVC.Chat.Data.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.FName).HasMaxLength(Constants.NameMaxLength);
            
            builder.Property(u => u.LName).HasMaxLength(Constants.NameMaxLength);

            builder.Property(u => u.FullName).HasComputedColumnSql("[FName]+' '+[LName]");

            builder.ComplexProperty(u => u.Picture,picture => 
            {
                picture.Property(p => p.Url).HasMaxLength(Picture.MaxUrlLength).HasColumnType("VarChar").IsRequired();
                picture.Property(p => p.Id).HasMaxLength(Picture.MaxIdLength).HasColumnType("VarChar").IsRequired();
            });
        }
    }
}
