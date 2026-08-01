using Microsoft.AspNetCore.Mvc;

namespace MVC.Chat.Controllers
{
    public class ChatController :Controller
    {
        public IActionResult Index()
        { 
            return View();
        }
    }
}
