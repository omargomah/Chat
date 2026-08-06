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

        public ChatHub(IUserConnectionRepository userConnectionRepository ,
            IUnitOfWork unitOfWork,
            IMessageRepository messageRepository)
        {
            _userConnectionRepository = userConnectionRepository;
            _unitOfWork = unitOfWork;
            _messageRepository = messageRepository;
        }
        public async Task SendMessageToUser(int receiverId, string message)
        {
            // 1. Extract the current authenticated user's ID
            var senderIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(senderIdClaim) || !int.TryParse(senderIdClaim, out int senderId))
                throw new HubException("User is not authenticated.");

            if (string.IsNullOrWhiteSpace(message))
                return;

            var sentAt = DateTime.UtcNow;

            var chatMessage = new Message
            {
                SenderId = senderId,
                ReceiverId  = receiverId,
                Content = message,
                SentAt = sentAt,
                IsRead = false
            };

            await _messageRepository.AddAsync(chatMessage);
            await _unitOfWork.SaveChangesAsync();

            var formattedTime = sentAt.ToLocalTime().ToString("hh:mm tt");

            //IEnumerable<string>  connectionsIdThatWillReceiveMessage = await _userConnectionRepository.GetAllConnectionsIdForThisUserIdAsync(receiverId);
            //if(!connectionsIdThatWillReceiveMessage.IsNullOrEmpty())
            //    await Clients.Clients(connectionsIdThatWillReceiveMessage).SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
            //await Clients.Caller.SendAsync("ReceiveMessage", Context.User.Identity.Name, message);

            await Clients.Users(receiverId.ToString(),senderIdClaim.ToString())
                .SendAsync("ReceiveMessage", senderId, message, formattedTime);
        }



        public override async Task OnConnectedAsync()
        {
            int userId = int.Parse(Context.GetHttpContext()?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _userConnectionRepository.AddAsync(UserConnection.Create(userId, Context.ConnectionId));

            if((await _unitOfWork.SaveChangesAsync()) == 0) 
                throw new InvalidDataException("there is problem in Save the Connection id to work as real time application");
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            UserConnection? userConnectionThatWillRemove = await _userConnectionRepository.GetByConnectionIdAsync(Context.ConnectionId);
            if (userConnectionThatWillRemove is not null)
            { 
                _userConnectionRepository.Remove(userConnectionThatWillRemove);
               await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
