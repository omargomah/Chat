using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Chat.Data;
using MVC.Chat.Dtos;
using MVC.Chat.Entities;
using MVC.Chat.Interfaces;
using MVC.Chat.Models;
using System.Security.Claims;

namespace MVC.Chat.Controllers
{
    [Authorize]
    public class ChatController :Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public ChatController(
            IUserRepository userRepository,
            IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            List<ChatSidebarUserViewModel> users = await _userRepository.GetUsersForSideBarAsync(currentUserId);
            
            ChatIndexViewModel model = new ChatIndexViewModel
            {
                CurrentUserId = currentUserId,
                CurrentUserName = User.Identity.Name,
                Users = users
            };

            return View(model);
        }

        [HttpGet("GetConversation")]
        public async Task<IActionResult> GetConversation([FromQuery]int targetUserId)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var messages = await _messageRepository.GetMessagesForConversation(currentUserId,targetUserId);

            return Json(messages);
        }
    }
}
