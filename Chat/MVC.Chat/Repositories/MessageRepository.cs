using Microsoft.EntityFrameworkCore;
using MVC.Chat.Data;
using MVC.Chat.Dtos;
using MVC.Chat.Entities;
using MVC.Chat.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace MVC.Chat.Repositories
{
    public class MessageRepository(ApplicationDbContext dbContext)
        : BaseRepository<Message>(dbContext), IMessageRepository
    {
        public async Task AddAsync(Message message, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(message, cancellationToken);
        }

        public async Task<Message?> GetByIdAsync(int messageId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.Id == messageId, cancellationToken);
        }

        public async Task MarkMessagesAsReadAsync(int currentUserId, int targetUserId, CancellationToken cancellationToken = default)
        {
            await _dbSet.Where(m => m.SenderId == targetUserId && m.ReceiverId == currentUserId && !m.IsRead)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsRead, true), cancellationToken);
        }
        public async Task<int> GetCountOfUnreadMessagesAsync(int senderId, int receiverId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(x => x.SenderId == senderId && x.ReceiverId == receiverId && !x.IsRead, cancellationToken);
        }
        public async Task<List<MessageDto>> GetMessagesForConversation(int currentUserId, int targetUserId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(m => !m.IsDeleted &&
                ((m.SenderId == currentUserId && m.ReceiverId == targetUserId)
                                || (m.SenderId == targetUserId && m.ReceiverId == currentUserId)))
                .OrderBy(m => m.SentAt)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    Content = m.Content,
                    SentAt = m.SentAt,
                    LastUpdatedAt = m.LastUpdatedAt,
                    IsRead = m.IsRead
                })
                .ToListAsync(cancellationToken);
        }
    }
}
