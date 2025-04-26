namespace Boostup.API.Entities.Dtos.Response
{
    public class EmployeeImageResponse
    {
        public string Name { get; set; } // Display Name For File

        public string? OriginalName { get; set; }

        public string? MimeType { get; set; }

        public double? Size { get; set; }

        public string? Path { get; set; }
    }
}
