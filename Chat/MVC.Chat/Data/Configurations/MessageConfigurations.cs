using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Chat.Entities;

namespace MVC.Chat.Data.Configurations
{
    public class MessageConfigurations : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);
           
            builder.HasIndex(m => new { m.SenderId, m.ReceiverId, m.SentAt });

            builder.Property(m => m.Content).HasMaxLength(Constants.ContentMaxLength).IsRequired();
            
            builder.Property(u => u.SentAt).HasDefaultValueSql("SYSDATETIME()");

            builder.Property(u => u.IsRead).HasDefaultValue(false);
            
            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
            
            //builder.HasOne(m => m.Conversation)
            //    .WithMany(c => c.Messages)
            //    .HasForeignKey(m => m.ConversationId);
            
            builder.HasOne(m => m.Sender)
                .WithMany(u => u.SendMessages)
                .HasForeignKey(m => m.SenderId);
            
            builder.HasOne(m => m.Receiver)
                .WithMany(c => c.ReceiveMessages)
                .HasForeignKey(m => m.ReceiverId);
        }
    }
}
