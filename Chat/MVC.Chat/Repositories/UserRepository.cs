using Microsoft.EntityFrameworkCore;
using MVC.Chat.Data;
using MVC.Chat.Entities;
using MVC.Chat.Interfaces;
using MVC.Chat.Models;

namespace MVC.Chat.Repositories
{
    public class UserRepository(
        ApplicationDbContext dbContext,
        IUserConnectionRepository userConnectionRepository,
        IMessageRepository messageRepository)
        : BaseRepository<User>(dbContext), IUserRepository
    {
        private readonly IUserConnectionRepository _userConnectionRepository = userConnectionRepository;
        private readonly IMessageRepository _messageRepository = messageRepository;

        public async Task<List<ChatSidebarUserViewModel>> GetUsersForSideBarAsync(int currentUserId, CancellationToken cancellationToken = default)
        {
            var result = await _dbSet.Where(u => u.Id != currentUserId)
                        .Select(u => new
                        {
                            User = u,
                            LastMsg = _dbContext.Messages
                                .Where(m => !m.IsDeleted && ((m.SenderId == currentUserId && m.ReceiverId == u.Id) ||
                                            (m.SenderId == u.Id && m.ReceiverId == currentUserId)))
                                .OrderByDescending(m => m.LastUpdatedAt ?? m.SentAt)
                                .Select(m => new { m.Content, m.SentAt, m.LastUpdatedAt })
                                .FirstOrDefault()
                        })
                        .Select(x => new ChatSidebarUserViewModel
                        {
                            UserId = x.User.Id,
                            FullName = x.User.FullName,
                            IsOnline = false,
                            LastMessage = x.LastMsg != null ? x.LastMsg.Content : string.Empty,
                            LastMessageTime = x.LastMsg != null ? x.LastMsg.LastUpdatedAt ?? x.LastMsg.SentAt : (DateTime?)null,

                        })
                        .AsNoTracking()
                        .ToListAsync();

            foreach (var item in result)
            {
                item.IsOnline = await _userConnectionRepository.IsOnlineAsync(item.UserId, cancellationToken);
                item.CountOfUnreadMessages = await _messageRepository.GetCountOfUnreadMessagesAsync(item.UserId, currentUserId, cancellationToken);
            }

            return result;


        }

    }
}
