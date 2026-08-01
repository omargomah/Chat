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

            builder.HasIndex(x => new { x.ParticipantBId, x.ParticipantAId }).IsUnique();
            
            builder.Property(m => m.LastMessageContent).HasMaxLength(Constants.ContentMaxLength).IsRequired();
                        
            builder.HasOne(m => m.ParticipantA)
                .WithMany(c => c.InitiatedConversations)
                .HasForeignKey(c => c.ParticipantAId);

            builder.HasOne(m => m.ParticipantB)
                .WithMany(c => c.ReceivedConversations)
                .HasForeignKey(c => c.ParticipantBId);

        }
    }
}
