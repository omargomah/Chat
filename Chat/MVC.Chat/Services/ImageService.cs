using Domain.Interfaces;
using Microsoft.Extensions.Options;
using MVC.Chat.Configurations;
namespace Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly ImageValidationConfigurations _settings;

        public ImageService(IOptionsSnapshot<ImageValidationConfigurations> settings)
        {
            _settings = settings.Value;
        }

        public (bool IsValid, string ErrorMessage) ValidateImage(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return (false, "No file uploaded.");

            if (file.Length > _settings.MaxFileSizeBytes)
                return (false, $"File size exceeds limit of {_settings.MaxFileSizeBytes / (1024 * 1024)} MB.");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_settings.AllowedExtensions.Contains(ext))
                return (false, $"Invalid file extension.");

            if (!_settings.AllowedMimeTypes.Contains(file.ContentType.ToLower()))
                return (false, "Invalid content type.");

            return (true, string.Empty);
        }
    }
}
