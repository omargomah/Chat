using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Chat.Entities;
using MVC.Chat.Models;

namespace MVC.Chat.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(registerViewModel);

            User user = new User() 
            {
                FName = registerViewModel.FName ,
                LName = registerViewModel.LName,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                PhoneNumber = registerViewModel.PhoneNumber,
            };

            IdentityResult identityResult = await _userManager.CreateAsync(user,registerViewModel.Password);

            if (identityResult.Succeeded)
                return RedirectToAction("Login", "Auth");

            foreach (var item in identityResult.Errors)
                ModelState.AddModelError(string.Empty, item.Description);
            
            return View(registerViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid)
                return View(loginViewModel);
            User? user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is Invalid");
                return View(loginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(
            user.UserName!,
            loginViewModel.Password,
            isPersistent: true,
            lockoutOnFailure: true);
            if (result.IsLockedOut)
            { 
                ModelState.AddModelError(string.Empty, "Email or Password is Invalid");
                return View(loginViewModel);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is Invalid");
                return View(loginViewModel);
            }
            return RedirectToAction("Index", "Chat");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // 1. Clears ASP.NET Core Identity authentication cookie
            await _signInManager.SignOutAsync();

            // 2. Redirect back to login page
            return RedirectToAction("Login", "Auth");
        }

    }
}
