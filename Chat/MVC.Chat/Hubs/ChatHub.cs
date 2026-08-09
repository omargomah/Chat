using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVC.Chat.Entities;
using MVC.Chat.Interfaces;
using System.Security.Claims;

namespace MVC.Chat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IUserConnectionRepository _userConnectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepository _messageRepository;

        public ChatHub(IUserConnectionRepository userConnectionRepository,
            IUnitOfWork unitOfWork,
            IMessageRepository messageRepository)
        {
            _userConnectionRepository = userConnectionRepository;
            _unitOfWork = unitOfWork;
            _messageRepository = messageRepository;
        }
        public async Task<int> SendMessageToUser(int receiverId, string message)
        {
            // 1. Extract the current authenticated user's ID
            var senderIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(senderIdClaim) || !int.TryParse(senderIdClaim, out int senderId))
                throw new HubException("User is not authenticated.");

            if (string.IsNullOrWhiteSpace(message))
                return 0;

            var sentAt = DateTime.UtcNow;

            var chatMessage = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = message,
                SentAt = sentAt,
                IsRead = false
            };
            CancellationToken cancellationToken = new CancellationToken();
            await _messageRepository.AddAsync(chatMessage, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var formattedTime = sentAt.ToLocalTime().ToString("hh:mm tt");

            await Clients.User(receiverId.ToString())
                .SendAsync("ReceiveMessage", chatMessage.Id, senderId, receiverId, message, formattedTime, (DateTime?)null);

            return chatMessage.Id;
        }

        public async Task UpdateMessage(int messageId, string newContent)
        {
            var senderIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(senderIdClaim) || !int.TryParse(senderIdClaim, out int senderId))
                throw new HubException("User is not authenticated.");

            var message = await _messageRepository.GetByIdAsync(messageId, CancellationToken.None);
            if (message is null)
                throw new HubException("Message not found.");
            if (message.SenderId != senderId)
                throw new HubException("Unauthorized.");

            message.Content = newContent;
            message.LastUpdatedAt = DateTime.UtcNow;
            message.IsRead = false;

            await _unitOfWork.SaveChangesAsync();

            var formattedTime = message.LastUpdatedAt.Value.ToLocalTime().ToString("hh:mm tt");

            await Clients.Users(message.ReceiverId.ToString(), senderIdClaim.ToString())
                .SendAsync("MessageUpdated", message.Id, message.SenderId, message.ReceiverId, message.Content, formattedTime);
        }

        public async Task DeleteMessage(int messageId)
        {
            var senderIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(senderIdClaim) || !int.TryParse(senderIdClaim, out int senderId))
                throw new HubException("User is not authenticated.");

            var message = await _messageRepository.GetByIdAsync(messageId, CancellationToken.None);
            if (message is null)
                throw new HubException("Message not found.");
            if (message.SenderId != senderId)
                throw new HubException("Unauthorized.");

            message.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();

            await Clients.Users(message.ReceiverId.ToString(), senderIdClaim.ToString())
                .SendAsync("MessageDeleted", message.Id, message.SenderId, message.ReceiverId);
        }


        public override async Task OnConnectedAsync()
        {
            int userId = int.Parse(Context.GetHttpContext()?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _userConnectionRepository.AddAsync(UserConnection.Create(userId, Context.ConnectionId));

            if ((await _unitOfWork.SaveChangesAsync()) == 0)
                throw new InvalidDataException("there is problem in Save the Connection id to work as real time application");

            await Clients.All.SendAsync("UserOnline", userId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            UserConnection? userConnectionThatWillRemove = await _userConnectionRepository.GetByConnectionIdAsync(Context.ConnectionId);
            if (userConnectionThatWillRemove is not null)
            {
                var userId = userConnectionThatWillRemove.UserId;
                _userConnectionRepository.Remove(userConnectionThatWillRemove);
                await _unitOfWork.SaveChangesAsync();

                var remainingConnections = await _userConnectionRepository.GetAllConnectionsIdForThisUserIdAsync(userId);
                if (!remainingConnections.Any())
                {
                    await Clients.All.SendAsync("UserOffline", userId);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
