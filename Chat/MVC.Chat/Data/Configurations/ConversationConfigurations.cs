using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Chat.Entities;

namespace MVC.Chat.Data.Configurations
{
    public class ConversationConfigurations : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.HasIndex(c => new { c.SenderId, c.ReceiverId })
                .IsUnique();
            
            builder.Property(m => m.LastMessageContent).HasMaxLength(Constants.ContentMaxLength).IsRequired();
                        
            builder.HasOne(m => m.Sender)
                .WithMany(c => c.Conversations)
                .HasForeignKey(m => m.SenderId);

            builder.HasOne(m => m.Receiver)
                .WithMany(c => c.Conversations)
                .HasForeignKey(m => m.ReceiverId);
        }
    }
}
