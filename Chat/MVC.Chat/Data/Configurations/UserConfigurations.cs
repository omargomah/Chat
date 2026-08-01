using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Chat.Entities;

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
        }
    }
}
