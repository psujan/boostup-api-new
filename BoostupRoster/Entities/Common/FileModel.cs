namespace Boostup.API.Entities.Common
{
    public class FileModel:Base
    {
        public string Name { get; set; } // Display Name For File

        public string? OriginalName { get; set; }

        public string? MimeType { get; set; }

        public double? Size { get; set; }

        public string? Path { get; set; }
    }
}
