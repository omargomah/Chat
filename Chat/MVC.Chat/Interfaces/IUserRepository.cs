using MVC.Chat.Dtos;
using MVC.Chat.Models;

namespace MVC.Chat.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<List<ChatSidebarUserViewModel>> GetUsersForSideBarAsync(int currentUserId,CancellationToken cancellationToken =default);

    }

}
