namespace monchotradebackend.Interface;
using monchotradebackend.service;
  public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string[] allowedFileExtensions, UploadType uploadType);
        void DeleteFile(string fileNameWithExtension, UploadType uploadType);
        string GetFileUrl(string fileName, UploadType uploadType);
    }