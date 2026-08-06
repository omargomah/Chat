using MVC.Chat.Dtos;
using MVC.Chat.Entities;

namespace MVC.Chat.Interfaces
{
    public interface IMessageRepository : IBaseRepository
    {
        Task<List<MessageDto>> GetMessagesForConversation(int currentUserId, int targetUserId);
        Task AddAsync(Message message);


    }

}
