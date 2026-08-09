using MVC.Chat.Dtos;
using MVC.Chat.Entities;

namespace MVC.Chat.Interfaces
{
    public interface IMessageRepository : IBaseRepository
    {
        Task<List<MessageDto>> GetMessagesForConversation(int currentUserId, int targetUserId, CancellationToken cancellationToken = default);
        Task AddAsync(Message message, CancellationToken cancellationToken = default);
        Task<Message?> GetByIdAsync(int messageId, CancellationToken cancellationToken = default);
        Task MarkMessagesAsReadAsync(int currentUserId, int targetUserId, CancellationToken cancellationToken = default);
        Task<int> GetCountOfUnreadMessagesAsync(int senderId, int receiverId, CancellationToken cancellationToken = default);
    }

}
