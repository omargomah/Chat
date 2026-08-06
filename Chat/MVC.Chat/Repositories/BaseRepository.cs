using Microsoft.EntityFrameworkCore;
using MVC.Chat.Data;
using MVC.Chat.Interfaces;

namespace MVC.Chat.Repositories
{
    public class BaseRepository<T>(ApplicationDbContext dbContext) : IBaseRepository where T : class
    {
        protected readonly ApplicationDbContext _dbContext = dbContext;
        protected readonly DbSet<T> _dbSet = dbContext.Set<T>();
    }
}
