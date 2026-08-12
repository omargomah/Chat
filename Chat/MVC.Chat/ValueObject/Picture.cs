using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Chat.ValueObject
{
    [ComplexType]
    public class Picture
    {
        public const int MaxUrlLength = 2048;
        public const int MaxIdLength = 500;

        private Picture(string url, string id)
        {
            Url = url;
            Id = id;
        }

        public string Url { get; private set; }
        public string Id { get; private set; }
        public static Picture Create(string url ,string id)
        {
            if(string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException("url");
            if(!Uri.TryCreate(url,UriKind.Absolute,out _))
                throw new ArgumentException($"'{url}' is not a valid absolute URL.", nameof(url));
            return new Picture(url,id);

        }
        public bool HasValue() => !string.IsNullOrWhiteSpace(Url);
        public override string ToString() => Url;
        public static implicit operator string?(Picture? picture) => picture?.Url;
    }
}
