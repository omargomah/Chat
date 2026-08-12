using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Chat.Entities;
using MVC.Chat.Interfaces;
using MVC.Chat.Models;
using MVC.Chat.ValueObject;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace MVC.Chat.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly ICloudinaryService _cloudinaryService;

        public UserProfileController(IUserRepository userRepository, IUnitOfWork unitOfWork, IImageService imageService, ICloudinaryService cloudinaryService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _cloudinaryService = cloudinaryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View((UserViewModel)await GetUser());
        }
        private async Task<User> GetUser()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
                throw new UnauthorizedAccessException();
            User? user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
                throw new UnauthorizedAccessException();
            return user!;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateNameData(UpdateNameDataViewModel updateNameDataViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid name data. Please try again.";
                return RedirectToAction(nameof(Index));
            }
            User userToUpdate = await GetUser();
            userToUpdate.UpdateNameData(updateNameDataViewModel.FName, updateNameDataViewModel.LName);
            if (await _unitOfWork.SaveChangesAsync() == 0)
            {
                TempData["Error"] = "Update Name Failed try again in another time";
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = "Name updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateImageOfUser(UpdateImageOfUserViewModel updateImageOfUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please select a valid image file.";
                return RedirectToAction(nameof(Index));
            }
            var result = _imageService.ValidateImage(updateImageOfUserViewModel.Image);
            if (!result.IsValid)
            {
                TempData["Error"] = result.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }
            User userToUpdate = await GetUser();
            Picture picture;
            try
            {
                picture = await _cloudinaryService.UploadImageAsync(updateImageOfUserViewModel.Image);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Failed to upload image: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
            if (userToUpdate.Picture is not null)
            {
                if (await _cloudinaryService.DeleteImageAsync(userToUpdate.Picture.Id))
                    TempData["Error"] = "Unable to Delete Old image try in another time or call Support to fix problem";

            }

            userToUpdate.UpdateImage(picture);
            if (await _unitOfWork.SaveChangesAsync() == 0)
            {
                TempData["Error"] = "Update image failed try again in another time";
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = "Profile picture updated successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
