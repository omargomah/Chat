using MVC.Chat.Entities;

namespace MVC.Chat.Interfaces
{
    public interface IUserConnectionRepository :IBaseRepository
    {
        Task AddAsync(UserConnection userConnection);
        Task<UserConnection?> GetByConnectionIdAsync(string connectionId);
        void Remove(UserConnection userConnection);
        Task<bool> IsOnlineAsync(int userId,CancellationToken cancellationToken =default);

        Task<IEnumerable<string>> GetAllConnectionsIdForThisUserIdAsync(int userId);

    }

}
