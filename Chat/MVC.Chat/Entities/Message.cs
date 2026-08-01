namespace MVC.Chat.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
        public DateTimeOffset SentAt { get; set; }
        public DateTimeOffset? LastUpdatedAt { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public User Sender { get; set; }
        public Conversation Conversation { get; set; }
    }
}
