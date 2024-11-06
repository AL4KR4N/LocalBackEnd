using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using monchotradebackend.Interface;

namespace monchotradebackend.service
{
    public enum UploadType
    {
        Profile,
        Product
    }

      public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private const int MaxFileSizeInMB = 10;
        private const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024; // 10MB

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        private string GetUploadPath(UploadType uploadType)
        {
            var wwwrootPath = _environment.WebRootPath;
            
            var uploadPath = uploadType switch
            {
                UploadType.Profile => Path.Combine(wwwrootPath, "uploads", "profiles"),
                UploadType.Product => Path.Combine(wwwrootPath, "uploads", "products"),
                _ => throw new ArgumentException($"Unsupported upload type: {uploadType}")
            };

            // Ensure directory exists
            Directory.CreateDirectory(uploadPath);
            return uploadPath;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string[] allowedFileExtensions, UploadType uploadType)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (file.Length <= 0)
            {
                throw new ArgumentException("File is empty", nameof(file));
            }

            if (file.Length > MaxFileSizeInBytes)
            {
                throw new ArgumentException($"File size exceeds maximum limit of {MaxFileSizeInMB}MB");
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedFileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    $"File type not allowed. Allowed types: {string.Join(", ", allowedFileExtensions)}");
            }

            var fileName = $"{Guid.NewGuid()}{extension}";
            var uploadPath = GetUploadPath(uploadType);
            var filePath = Path.Combine(uploadPath, fileName);

            try
            {
                await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                await file.CopyToAsync(stream);
                return fileName;
            }
            catch (Exception ex)
            {
                // Clean up the file if it was created
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                throw new IOException($"Error saving file: {ex.Message}", ex);
            }
        }

        public void DeleteFile(string fileNameWithExtension, UploadType uploadType)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentNullException(nameof(fileNameWithExtension));
            }

            var uploadPath = GetUploadPath(uploadType);
            var filePath = Path.Combine(uploadPath, fileNameWithExtension);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {fileNameWithExtension}");
            }

            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                throw new IOException($"Error deleting file: {ex.Message}", ex);
            }
        }

        public string GetFileUrl(string fileName, UploadType uploadType)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            // Return the URL path instead of physical path
            var urlPath = uploadType switch
            {
                UploadType.Profile => $"/uploads/profiles/{fileName}",
                UploadType.Product => $"/uploads/products/{fileName}",
                _ => throw new ArgumentException($"Unsupported upload type: {uploadType}")
            };

            return urlPath;
        }
    }
}