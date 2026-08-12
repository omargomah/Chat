using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MVC.Chat.ValueObject;

namespace Domain.Interfaces
{
    public interface ICloudinaryService
    {
        Task<Picture> UploadImageAsync(IFormFile file, CancellationToken cancellationToken = default);
        Task<bool> DeleteImageAsync(string publicId, CancellationToken cancellationToken = default);
    }
}
