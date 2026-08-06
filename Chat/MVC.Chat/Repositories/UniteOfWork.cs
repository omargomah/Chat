using MVC.Chat.Data;
using MVC.Chat.Interfaces;

namespace MVC.Chat.Repositories
{
    public class UniteOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
