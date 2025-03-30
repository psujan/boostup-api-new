using Boostup.API.Entities.Common;
using Boostup.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Boostup.API.Services
{
    public class FileUploadService : IFileUploadService
    {
        public readonly static string Default_Upload_Dir = "wwwroot";

        private readonly IHostEnvironment environment;
        private readonly ILogger<FileUploadService> logger;

        public FileUploadService(IHostEnvironment env , ILogger<FileUploadService> logger)
        {
            this.environment = env;
            this.logger = logger;
        }

        public void DeleteFileIfExists(string filePathInDirectory)
        {
            string storagePath = Path.Combine(environment.ContentRootPath, Default_Upload_Dir);
            // Combine the directory path and file name to get the full file path
            string filePath = Path.Combine(storagePath, filePathInDirectory);

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Delete the file
                File.Delete(filePath);
            }
        }

        public async Task<FileResponse?> UploadFile(IFormFile file, string? modelName, string uploadDir = null)
        {
            try
            {
                if (file.Length <= 0)
                {
                    return null;
                }
                var uploadDirectory =  Default_Upload_Dir + $"/{uploadDir}/" ;

                string storagePath = Path.Combine(environment.ContentRootPath, uploadDirectory);

                // Create the directory if it doesn't exist
                if (!Directory.Exists(storagePath))
                {
                    Directory.CreateDirectory(storagePath);
                }
                string renamed = modelName != null ? modelName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                                    : file.FileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var filePath = Path.Combine( uploadDirectory , renamed);

                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return new FileResponse()
                {
                    FileName = renamed,
                    OriginalName = file.FileName,
                    ByteSize = file.Length,
                    Extension = Path.GetExtension(filePath),
                    Path = uploadDir +"/"+renamed
                };
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in uploading file: " + ex.ToString());
                return null;
            }
        }
    }
}
