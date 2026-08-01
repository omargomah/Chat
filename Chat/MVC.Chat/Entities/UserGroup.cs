namespace MVC.Chat.Entities
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public string GroupName { get; set; }
        public User User { get; set; }
    }

}
