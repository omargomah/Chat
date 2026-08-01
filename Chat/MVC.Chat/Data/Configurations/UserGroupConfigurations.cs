using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Chat.Entities;

namespace MVC.Chat.Data.Configurations
{
    public class UserGroupConfigurations : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(x => new {x.UserId ,x.GroupName });

            builder.HasIndex(x => x.UserId).IsUnique(false);
            
            builder.HasOne(m => m.User)
                .WithMany(c => c.UserGroups)
                .HasForeignKey(m => m.UserId);
        }
    }
}
