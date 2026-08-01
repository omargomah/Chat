namespace MVC.Chat.Entities
{
    public class Conversation
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string LastMessageContent { get; set; }
        public DateTimeOffset LastMessageSentAt { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
