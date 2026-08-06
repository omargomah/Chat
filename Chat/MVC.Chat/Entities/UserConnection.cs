namespace MVC.Chat.Entities
{
    public class UserConnection
    {
        private UserConnection(int userId , string connectionId)
        {
            UserId = userId ;
            ConnectionId = connectionId ;
        }
        public int UserId { get; private set; }
        public string ConnectionId { get; private set; }
        public User  User { get; set; }

        public static UserConnection Create(int userId, string connectionId)
        {
            return new UserConnection(userId, connectionId);
        }
    }
}
