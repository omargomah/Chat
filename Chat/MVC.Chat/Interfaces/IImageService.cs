namespace Domain.Interfaces
{
    public interface IImageService
    {
        (bool IsValid, string ErrorMessage) ValidateImage(IFormFile file);   
    }
}
