namespace MVC.Chat.Models
{
    public class ChatSidebarUserViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public bool IsOnline { get; set; }
        public string LastMessage { get; set; }
        public DateTime? LastMessageTime { get; set; }
    }
}
