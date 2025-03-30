using Boostup.API.Entities.Common;

namespace Boostup.API.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task<FileResponse?> UploadFile(IFormFile file, string? model, string uploadDir);

        void DeleteFileIfExists(string filePath);
    }
}
