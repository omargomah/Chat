using Microsoft.AspNetCore.Identity;

namespace MVC.Chat.Entities
{
    public class User:IdentityUser<int>
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string FullName { get;}
        public ICollection<Conversation> InitiatedConversations { get; set; } = new List<Conversation>();
        public ICollection<Conversation> ReceivedConversations { get; set; } = new List<Conversation>();
        public ICollection<UserConnection> UserConnections { get; set; } = new HashSet<UserConnection>();
        public ICollection<UserGroup> UserGroups { get; set; } = new HashSet<UserGroup>();
        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }

}
