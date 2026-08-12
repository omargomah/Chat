using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Chat.Data;
using MVC.Chat.Dtos;
using MVC.Chat.Entities;
using MVC.Chat.Interfaces;
using MVC.Chat.Models;
using MVC.Chat.Repositories;
using System.Security.Claims;

namespace MVC.Chat.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChatController(
            IUserRepository userRepository,
            IMessageRepository messageRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var currentUser = await _userRepository.GetByIdAsync(currentUserId);
            List<ChatSidebarUserViewModel> users = await _userRepository.GetUsersForSideBarAsync(currentUserId);

            ChatIndexViewModel model = new ChatIndexViewModel
            {
                CurrentUserId = currentUserId,
                CurrentUserName = User.Identity.Name!,
                CurrentUserPictureUrl = currentUser?.Picture?.Url ?? string.Empty,
                Users = users
            };

            return View(model);
        }

        [HttpGet("Chat/GetConversation")]
        public async Task<IActionResult> GetConversation([FromQuery] int targetUserId, CancellationToken cancellationToken = default)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var messages = await _messageRepository.GetMessagesForConversation(currentUserId, targetUserId, cancellationToken);

            await _messageRepository.MarkMessagesAsReadAsync(currentUserId, targetUserId, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return Json(messages);
        }
    }
}
