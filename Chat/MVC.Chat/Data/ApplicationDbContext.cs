using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC.Chat.Entities;

namespace MVC.Chat.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,IdentityRole<int>,int>
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Conversation> Conversations => Set<Conversation>();
        public DbSet<UserConnection> UserConnections => Set<UserConnection>();
        public DbSet<UserGroup> UserGroups => Set<UserGroup>();
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
