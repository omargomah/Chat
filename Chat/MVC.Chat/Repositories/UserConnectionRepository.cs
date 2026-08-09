using Microsoft.EntityFrameworkCore;
using MVC.Chat.Data;
using MVC.Chat.Entities;
using MVC.Chat.Interfaces;

namespace MVC.Chat.Repositories
{
    public class UserConnectionRepository(ApplicationDbContext dbContext) 
        : BaseRepository<UserConnection>(dbContext), IUserConnectionRepository
    {
        public void Remove(UserConnection userConnection)
        {
            _dbSet.Remove(userConnection);
        }
        public async Task AddAsync(UserConnection userConnection)
        {
            await _dbSet.AddAsync(userConnection);
        }
        public async Task<bool> IsOnlineAsync(int userId ,CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(uc => uc.UserId == userId, cancellationToken);
        }
        public async Task<UserConnection?> GetByConnectionIdAsync(string connectionId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.ConnectionId == connectionId);            
        }
        public async Task<IEnumerable<string>> GetAllConnectionsIdForThisUserIdAsync(int userId)
        {
             return await _dbSet.Where(x => x.UserId == userId).Select(x => x.ConnectionId).ToListAsync();
        }
    }
}
