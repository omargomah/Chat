using Microsoft.EntityFrameworkCore;
using MVC.Chat.Data;
using MVC.Chat.Entities;
using MVC.Chat.Interfaces;
using MVC.Chat.Models;

namespace MVC.Chat.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext)
        : BaseRepository<User>(dbContext), IUserRepository
    {
        
        public async Task<List<ChatSidebarUserViewModel>> GetUsersForSideBarAsync(int currentUserId)
        {
            return await _dbSet.Where(u => u.Id != currentUserId)
                        .Select(u => new
                        {
                            User = u,
                            LastMsg = _dbContext.Messages
                                .Where(m => (m.SenderId == currentUserId && m.ReceiverId == u.Id) ||
                                            (m.SenderId == u.Id && m.ReceiverId == currentUserId))
                                .OrderByDescending(m => m.SentAt)
                                .Select(m => new { m.Content, m.SentAt })
                                .FirstOrDefault()
                        })
                        .Select(x => new ChatSidebarUserViewModel
                        {
                            UserId = x.User.Id,
                            FullName = x.User.FullName,
                            IsOnline = false,
                            LastMessage = x.LastMsg != null ? x.LastMsg.Content : string.Empty,
                            LastMessageTime = x.LastMsg != null ? x.LastMsg.SentAt : (DateTime?)null
                        })
                        .AsNoTracking()
                        .ToListAsync();
        }

    }
}
