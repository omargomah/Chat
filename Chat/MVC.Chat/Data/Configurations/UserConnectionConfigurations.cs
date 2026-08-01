using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Chat.Entities;

namespace MVC.Chat.Data.Configurations
{
    public class UserConnectionConfigurations : IEntityTypeConfiguration<UserConnection>
    {
        public void Configure(EntityTypeBuilder<UserConnection> builder)
        {
            builder.HasKey(x => new {x.UserId ,x.ConnectionId });

            builder.HasIndex(x => x.UserId).IsUnique(false);
            
            builder.HasOne(m => m.User)
                .WithMany(c => c.UserConnections)
                .HasForeignKey(m => m.UserId);
        }
    }
}
