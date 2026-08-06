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
        public async Task AddAsync(Message message)
        {
            await _dbSet.AddAsync(message);
        }

        public async Task<List<MessageDto>> GetMessagesForConversation(int currentUserId , int targetUserId )
        {
            return await _dbSet.Where(m => (m.SenderId == currentUserId && m.ReceiverId == targetUserId) 
                                || (m.SenderId == targetUserId && m.ReceiverId == currentUserId))
                .OrderBy(m => m.SentAt)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    Content = m.Content,
                    SentAt = m.SentAt
                })
                .ToListAsync();
        }
    }
}
