using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using MVC.Chat.Configurations;
using MVC.Chat.ValueObject;
namespace Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptionsSnapshot<CloudinaryConfigurations> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<Picture> UploadImageAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File cannot be null or empty", nameof(file));

            await using var stream = file.OpenReadStream();

            var publicId = $"images/{Guid.NewGuid()}"; 

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                PublicId = publicId,
                Overwrite = true,
                Transformation = new Transformation().Quality("auto").FetchFormat("auto")
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

            if (uploadResult.Error != null)
                throw new InvalidOperationException($"Cloudinary upload failed: {uploadResult.Error.Message}");
            Picture picture;
            try
            {

               picture = Picture.Create(uploadResult.SecureUrl.ToString(), uploadResult.PublicId)!;
            }
            catch (Exception)
            {
                throw;
            }
            return picture;
        }

        public async Task<bool> DeleteImageAsync(string publicId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(publicId))
                return false;

            var deleteParams = new DeletionParams(publicId) 
            {
                ResourceType = ResourceType.Image 
            };
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }
    }
}
