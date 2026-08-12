namespace MVC.Chat.Configurations
{
    public class ImageValidationConfigurations
    {
        public int MaxFileSizeBytes { get; set; }
        public string[] AllowedExtensions { get; set; }
        public string[] AllowedMimeTypes { get; set; }
    }

}
