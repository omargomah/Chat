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

            builder.Property(m => m.Content).HasMaxLength(Constants.ContentMaxLength).IsRequired();
            
            builder.Property(u => u.SentAt).HasDefaultValue("SYSDATETIMEOFFSET()");

            builder.Property(u => u.IsRead).HasDefaultValue(false);
            
            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
            
            builder.HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.Id);
        }
    }
}
