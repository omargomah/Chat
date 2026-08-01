namespace MVC.Chat.Entities
{
    public class Conversation
    {
        public int Id { get; set; }
        public int ParticipantAId { get; set; }
        public int ParticipantBId { get; set; }
        public string LastMessageContent { get; set; }
        public DateTimeOffset LastMessageSentAt { get; set; }
        public User ParticipantA { get; set; }
        public User ParticipantB { get; set; }
        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
