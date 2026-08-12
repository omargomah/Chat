using MVC.Chat.Dtos;
using MVC.Chat.Entities;
using MVC.Chat.Models;

namespace MVC.Chat.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<List<ChatSidebarUserViewModel>> GetUsersForSideBarAsync(int currentUserId,CancellationToken cancellationToken =default);
        Task<User?> GetByIdAsync(int userId,CancellationToken cancellationToken = default);
    }

}
