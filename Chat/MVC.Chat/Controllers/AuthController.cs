using Microsoft.AspNetCore.Mvc;

namespace MVC.Chat.Controllers
{
    public class AuthController : Controller
    {
        #region Register
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var categories = await _mediator.Send(new GetAllCategoriesForCheckBoxQuery());

            return View(new RegisterViewModel()
            {
                AvailableCategories = categories
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(registerViewModel);

            string confirmationBaseUrl = Url.Action("ConfirmEmail", "Auth", null, Request.Scheme)!;

            var command = new RegisterUserCommand(
                registerViewModel.FullName,
                registerViewModel.Email,
                registerViewModel.Password,
                registerViewModel.Role, confirmationBaseUrl,
                registerViewModel.SelectedCategoryIds);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return RedirectToAction("RegisterConfirmation");

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error);

            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult RegisterConfirmation()
        {
            return View();
        }

        #endregion

    }
}
