namespace MVC.Chat.Models
{
    public class ChatIndexViewModel
    {
        public int CurrentUserId { get; set; }
        public string CurrentUserName { get; set; }
        public List<ChatSidebarUserViewModel> Users { get; set; } = new();
    }
}
