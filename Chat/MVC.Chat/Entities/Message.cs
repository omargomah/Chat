namespace MVC.Chat.Entities
{
    public class Message
    {
  
        public int Id { get; set; }
        //public int ConversationId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        //public Conversation Conversation { get; set; }

    }
}
