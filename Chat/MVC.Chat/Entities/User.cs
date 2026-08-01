using Microsoft.AspNetCore.Identity;

namespace MVC.Chat.Entities
{
    public class User:IdentityUser<int>
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string FullName { get;}
        public ICollection<Conversation> Conversations { get; set; } = new HashSet<Conversation>();
        public ICollection<UserConnection> UserConnections { get; set; } = new HashSet<UserConnection>();
    }

}
